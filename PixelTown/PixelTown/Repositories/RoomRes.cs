using Microsoft.AspNetCore.Http;
using PixelTown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelTown.Repositories
{
    public class RoomRes
    {
        public static Room createRoom(string userId, int mapId, string roomName, string roomPass, int quantity, string description)
        {
            Room room = new Room();
            room.Id = Guid.NewGuid().ToString("N");
            room.UserId = userId;
            room.MapId = mapId;
            room.RoomName = roomName;
            room.RoomPass = roomPass;
            room.Quantity = quantity;
            room.Description = description;

            using (var context = new PixelTownContext())
            {
                context.Room.Add(room);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return room;
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool joinRoom(string roomId, string userId, int characterId, DateTime time)
        {
            using (var context = new PixelTownContext())
            {
                var room = context.Room.Where(s => s.Id.Equals(roomId)).SingleOrDefault();
                var joinRoom = context.UserJoinRoom.Where(s => s.RoomId.Equals(roomId)).ToList();
                if (room == null)
                {
                    return false;
                } else
                {
                    
                    if (joinRoom.Count < room.Quantity)
                    {
                        var userAlreadyRoom = context.UserJoinRoom.Where(s => s.UserId.Equals(userId) && s.RoomId.Equals(roomId)).SingleOrDefault();
                        if (userAlreadyRoom == null)
                        {
                            Console.WriteLine(roomId + "  " + userId + " " + characterId);
                            UserJoinRoom user = new UserJoinRoom();
                            user.Time = time;
                            user.RoomId = roomId;
                            user.UserId = userId;
                            user.CharacterId = characterId;
                            user.State = "Online";
                            context.UserJoinRoom.Add(user);
                            var result = context.SaveChanges();
                            if (result > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        } else
                        {
                            userAlreadyRoom.CharacterId = characterId;
                            userAlreadyRoom.State = "Online";
                            var result = context.SaveChanges();
                            return true;
                        }
                    } else
                    {
                        return false;
                    }
                }
            }
        }

        public static object worldRoom()
        {
            using (var context = new PixelTownContext())
            {
                var allRoom = from room in context.Room select new { 
                    room.Id,
                    room.MapId,
                    room.UserId,
                    room.RoomName,
                    room.RoomPass,
                    room.Quantity,
                    room.Description,
                    UserJoinRoom = context.UserJoinRoom.Where(s => s.RoomId == room.Id).ToList(),
                };
                return allRoom.ToList();
            }
        }

        public static object myRoom(string userId)
        {
            var result_create = new List<object>();
            var result_join = new List<object>();
            using (var context = new PixelTownContext())
            {
                result_create = (from room in context.Room
                                 where room.UserId == userId
                                select new
                                {
                                    room.Id,
                                    room.MapId,
                                    room.UserId,
                                    room.RoomName,
                                    room.RoomPass,
                                    room.Quantity,
                                    room.Description,
                                    UserJoinRoom = context.UserJoinRoom.Where(s => s.RoomId == room.Id).ToList(),
                                }).ToList<object>();
            }
            using (var context = new PixelTownContext())
            {
                //result_join = context.UserJoinRoom.Where(s => s.UserId.Equals(userId)).ToList();
                result_join = (from c in context.UserJoinRoom
                              join room in context.Room
                              on c.RoomId equals room.Id where c.UserId == userId
                                select new
                                {
                                    room.Id,
                                    room.MapId,
                                    room.UserId,
                                    room.RoomName,
                                    room.RoomPass,
                                    room.Quantity,
                                    room.Description,
                                    UserJoinRoom = context.UserJoinRoom.Where(s => s.RoomId == room.Id).ToList(),
                                }).ToList<object>();
            }
            var result = new { create = result_create, join = result_join };
            return result;
        }

        public static object userInRoom(string roomId)
        {
            using (var context = new PixelTownContext())
            {
                var result = (from user_in_room in context.UserJoinRoom
                              join user in context.Account
                              on user_in_room.UserId equals user.Id where user_in_room.RoomId == roomId
                              select new
                              {
                                  name = user.Name,
                                  userId = user.Id,
                                  avatar = user.Avatar,
                                  state = user_in_room.State,
                                  time = user_in_room.Time,
                                  characterId = user_in_room.CharacterId
                              }).ToList<object>();
                return result;
            }
        }

        public static Room updateRoom(string id, int mapId, string roomName, string roomPass, string desc)
        {
            using (var context = new PixelTownContext())
            {
                Room room = context.Room.Where(s => s.Id.Equals(id)).SingleOrDefault();
                room.MapId = mapId;
                room.RoomName = roomName;
                room.RoomPass = roomPass;
                room.Description = desc;
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return room;
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool deleteRoom(string id)
        {
            using (var context = new PixelTownContext())
            {
                var room = context.Room.Where(s => s.Id.Equals(id)).SingleOrDefault();
                if (room != null)
                {
                    context.Room.Remove(room);
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return false;
                }
            }
        }

        public static bool checkUserInRoom(string roomId, string userId)
        {
            using (var context = new PixelTownContext())
            {
                var room = context.UserJoinRoom.Where(s => s.RoomId.Equals(roomId) && s.UserId.Equals(userId)).SingleOrDefault();
                if (room != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void setUserOffline(string roomId, string userId)
        {
            using (var context = new PixelTownContext())
            {
                var userInRoom = context.UserJoinRoom.Where(s => s.RoomId.Equals(roomId) && s.UserId.Equals(userId)).SingleOrDefault();
                if (userInRoom != null)
                {
                    userInRoom.State = "Offline";
                    context.SaveChanges();
                }
            }
        }
    }
}
