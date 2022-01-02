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
                        var userAlreadyRoom = context.UserJoinRoom.Where(s => s.UserId.Equals(userId)).SingleOrDefault();
                        if (userAlreadyRoom == null)
                        {
                            UserJoinRoom user = new UserJoinRoom();
                            user.Time = time;
                            user.RoomId = roomId;
                            user.UserId = userId;
                            user.CharacterId = characterId;
                            user.State = "Offline";
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
                            var userInRoom = context.UserJoinRoom.Where(s => s.UserId.Equals(userId)).SingleOrDefault();
                            userInRoom.CharacterId = characterId;
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

        public static List<Room> worldRoom()
        {
            using (var context = new PixelTownContext())
            {
                var room = context.Room;
                return room.ToList();
            }
        }

        public static object myRoom(string userId)
        {
            List<Room> result_create = new List<Room>();
            List<Room> result_join = new List<Room>();
            using (var context = new PixelTownContext())
            {
                result_create = context.Room.Where(s => s.UserId.Equals(userId)).ToList();
            }
            using (var context = new PixelTownContext())
            {
                //result_join = context.UserJoinRoom.Where(s => s.UserId.Equals(userId)).ToList();
                result_join = (from c in context.UserJoinRoom
                              join a in context.Room
                              on c.UserId equals a.UserId where c.RoomId == a.Id
                              select a).ToList();
            }
            var result = new { create = result_create, join = result_join };
            return result;
        }

        public static object userInRoom(string roomId)
        {
            using (var context = new PixelTownContext())
            {
                var result = (from c in context.UserJoinRoom
                              join a in context.Account
                              on c.RoomId equals roomId where c.UserId == a.Id
                              select new { 
                                  user = a,
                                  state = c.State
                              }).ToList();
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
    }
}
