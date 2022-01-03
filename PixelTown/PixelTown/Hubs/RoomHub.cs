using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PixelTown.Hubs.HubModels;
using PixelTown.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace PixelTown.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoomHub : Hub
    {
        private static RoomManager roomManager = new RoomManager();
        [Authorize]
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User "+Context.ConnectionId+" has been connected.");
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            var emptyRoom = new List<string>();
            var roomlist = roomManager.GetAllRooms();
            foreach(RoomInfo room in roomlist)
            {
                if(room.Users.GetUser(Context.ConnectionId) != null)
                {
                    //Get user Id from token
                    var identity = (ClaimsIdentity)Context.User.Identity;
                    var userId = identity.FindFirst(ClaimTypes.Name).Value;
                    RoomRes.setUserOffline(room.RoomId,userId);

                    var user = room.Users.GetUser(Context.ConnectionId);
                    Clients.Group(room.RoomId).SendAsync("UserOut",new {signalrID = user.signalrID });
                    room.Users.Remove(Context.ConnectionId);
                    Console.WriteLine("Number of user in room "+room.RoomId+" is "+room.Users.Count);
                }
                if(room.Users.Count == 0)
                {
                    emptyRoom.Add(room.RoomId);
                }
            }
            foreach(string roomId in emptyRoom)
            {
                roomManager.DeleteRoom(roomId);
            }
            Console.WriteLine("User " + Context.ConnectionId + " has been disconnected.");
            return base.OnDisconnectedAsync(exception);
        }

        /*
         * Vô phòng hoặc tạo phòng trong trường hợp chưa có phòng trống nào
         * Input: Tên phòng
         * Ouput: Tạo ra phòng mới, trong trường hợp có rồi thì trả về lỗi
         * */

        public async Task Join(string roomId, string character)
        {
            Console.WriteLine(Context.ConnectionId + " " + roomId);
            //Get user Id from token
            var identity = (ClaimsIdentity)Context.User.Identity;
            var userId = identity.FindFirst(ClaimTypes.Name).Value;
   
            //Get user all name
            var user = AccountRes.getById(userId);
            string name = user.Name;
            string hostId = user.Id;
            Console.WriteLine("claimUser: " + userId + ", Name:" + name);


            RoomInfo roomInfo = roomManager.GetRoomInfor(roomId);
            if(roomInfo != null)
            {
                roomInfo.Users.Add(Context.ConnectionId, new ConnectionInfor(Context.ConnectionId, name, character));
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                var allUsers = roomInfo.Users.GetAll();
                Console.WriteLine(allUsers.Count);
                await Clients.Caller.SendAsync("joined", new{
                    roomInfor = roomInfo,
                    Users = allUsers
                });
                await Clients.Group(roomId).SendAsync("ready");
            }
            else
            {
                RoomInfo newRoom = roomManager.CreateRoom(roomId, hostId, name);
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                newRoom.Users.Add(Context.ConnectionId, new ConnectionInfor(Context.ConnectionId, name, character));
                var allUsers = newRoom.Users.GetAll();
                await Clients.Caller.SendAsync("joined", new
                {
                    roomInfor = newRoom,
                    Users = allUsers
                });
                await Clients.Group(roomId).SendAsync("ready");
            }
        }

        public async Task NewUser(string roomId, float x, float y)
        {
            Console.WriteLine(Context.ConnectionId+" " +roomId);
            RoomInfo roomInfo = roomManager.GetRoomInfor(roomId);
            if (roomInfo != null)
            {
                var user = roomInfo.Users.GetUser(Context.ConnectionId);
                user.position = new Vector(x, y);
                await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("NewUserEntry", user);
            }
            else
            {
                await Clients.Caller.SendAsync("error", "This room does not exist!!!");
            }
        }

        public async Task GetAllPlayer(string roomId)
        {
            RoomInfo roomInfo = roomManager.GetRoomInfor(roomId);
            if (roomInfo != null)
            {
                var allUsers = roomInfo.Users.GetAll();
                Console.WriteLine(allUsers.Count);
                await Clients.Caller.SendAsync("AllPlayer", new
                {
                    users = allUsers
                });
            }
            else
            {
                await Clients.Caller.SendAsync("error", "This room does not exist!!!");
            }
        }

        public async Task Move(string roomId, float x, float y)
        {
            RoomInfo roomInfo = roomManager.GetRoomInfor(roomId);
            if (roomInfo != null)
            {
                var user = roomInfo.Users.GetUser(Context.ConnectionId);
                if(user != null)
                {
                    user.position = new HubModels.Vector(x, y);
                    await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("UserMoving", new
                    {
                        signalrID = Context.ConnectionId,
                        position = user.position,
                        time = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds()
                    });
                }
                else
                {
                    await Clients.Caller.SendAsync("error", "Something has been error");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("error", "This room does not exist!!!");
            }
        }

        public async Task Stop(string roomId, float x, float y)
        {
            RoomInfo roomInfo = roomManager.GetRoomInfor(roomId);
            if (roomInfo != null)
            {
                var user = roomInfo.Users.GetUser(Context.ConnectionId);
                user.position = new HubModels.Vector(x, y);
                await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("UserStop", new
                {
                    signalrID = Context.ConnectionId,
                    position = user.position,
                    time = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds()
                });
            }
            else
            {
                await Clients.Caller.SendAsync("error", "This room does not exist!!!");
            }
        }

        public async Task ShortMessage(string roomId, string message)
        {
            RoomInfo roomInfo = roomManager.GetRoomInfor(roomId);
            if (roomInfo != null)
            {
                await Clients.Caller.SendAsync("MyShortMessage", new
                {
                    message = message
                });
                await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("UserSendShortMessage", new
                {
                    signalrID = Context.ConnectionId,
                    message = message
                });
            }
            else
            {
                await Clients.Caller.SendAsync("error", "This room does not exist!!!");
            }

        }

        public async Task LongMessage(string roomId, string message)
        {
            Console.WriteLine(roomId + "  " + message);
            RoomInfo roomInfo = roomManager.GetRoomInfor(roomId);
            //Get user Id from token
            var identity = (ClaimsIdentity)Context.User.Identity;
            var userId = identity.FindFirst(ClaimTypes.Name).Value;
            var user = AccountRes.getById(userId);
            if (roomInfo != null && userId != null && user != null)
            {
               if (MessageRes.AddMessage(roomId, userId, message))
                {
                    await Clients.Group(roomId).SendAsync("UserSendLongMessage", new
                    {
                        userId = user.Id,
                        name = user.Name,
                        avatar = user.Avatar,
                        message = message,
                        time = DateTimeOffset.Now.ToUnixTimeMilliseconds()
                    });
                }
                else
                {
                    await Clients.Caller.SendAsync("error", "Something has been error");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("error", "This room does not exist!!!");
            }

        }


        public async Task GetAllRoom()
        {
            List<RoomInfo> list = roomManager.GetAllRooms();
            var newlist = from room in list select new{roomId = room.RoomId,host = room.HostId, users = room.Users.GetAll().ToList()};

            if (list != null)
            {
                await Clients.Caller.SendAsync("allroom", newlist);
            }
            else
            {
                await Clients.Caller.SendAsync("error", "There no room here");
            }

        }
    }
}
