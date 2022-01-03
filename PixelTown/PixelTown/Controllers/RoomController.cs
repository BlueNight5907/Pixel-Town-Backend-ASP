using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelTown.Models;
using PixelTown.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelTown.Controllers
{
    public class RoomController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("api/room")]
        public ActionResult<IEnumerable<Room>> GetWorldRoom()
        {
            var result = RoomRes.worldRoom();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("api/room/myRoom")]
        public ActionResult<IEnumerable<object>> GetMyRoom()
        {
            var claimUser = User.FindFirst(ClaimTypes.Name).Value;
            var result = RoomRes.myRoom(claimUser);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("api/room/people/{roomId}")]
        public ActionResult<IEnumerable<Account>> UserInRoom(string roomId)
        {
            Console.WriteLine(roomId);
            var result = RoomRes.userInRoom(roomId);
            if (result == null)
            {
                return BadRequest(new JsonResult(result));
            } else
            {
                return Ok(new JsonResult(result));
            }
        }

        // POST api/<RoomController>
        [HttpPost]
        [Authorize]
        [Route("api/room")]
        public IActionResult Post([FromForm] int mapId, string roomName, string roomPass, int quantity, string description)
        {
            var message = "";
            if (mapId == 0)
            {
                message = "Please select map!";
            } else if (roomName == "" || roomName == null)
            {
                message = "Please enter room name!";
            } else if (quantity <= 0)
            {
                message = "The number of people cannot be less than or equal to 0!";
            }

            if (message == "") 
            {
                var claimUser = User.FindFirst(ClaimTypes.Name).Value;

                var room = RoomRes.createRoom(claimUser, mapId, roomName, roomPass, quantity, description);
                if (room != null)
                {
                    return Ok(room);
                }
                else
                {
                    return BadRequest(room);
                }
            } else
            {
                return BadRequest(new JsonResult(message));
            }           
        }

        [HttpPost]
        [Authorize]
        [Route("api/room/{roomId}")]
        public IActionResult joinRoom(string roomId, [FromBody] UserJoinRoom join)
        {
            var message = "";
            if (join.CharacterId == 0)
            {
                message = "Please select character!";
            }

            if (message == "")
            {
                var claimUser = User.FindFirst(ClaimTypes.Name).Value;
                var result = RoomRes.joinRoom(roomId, claimUser, (int)join.CharacterId, DateTime.Now);
                if (result)
                {
                    Room room = new Room();
                    var userJoinRoom = new List<object>();
                    using (var context = new PixelTownContext())
                    {
                        room = context.Room.Where(s => s.Id.Equals(roomId)).SingleOrDefault();
                    }
                    using (var context = new PixelTownContext())
                    {
                        userJoinRoom = (from user_in_room in context.UserJoinRoom join user in context.Account 
                                        on user_in_room.UserId equals user.Id
                                       where user_in_room.RoomId == roomId
                                        select new { 
                                            name = user.Name,
                                            userId = user.Id,
                                            avatar = user.Avatar,
                                            state = user_in_room.State,
                                            time = user_in_room.Time,
                                            characterId = user_in_room.CharacterId
                                        }).ToList<object>();
                    }
                    var finalResult = new { room = room, userInRoom = userJoinRoom };
                    return Ok(new JsonResult(finalResult));
                }
                else
                {
                    return BadRequest("join room failed - Id room invalid!");
                }
            } else
            {
                return BadRequest(new JsonResult(message));
            }       
        }

        [HttpPut("api/room/{id}")]
        [Authorize]
        public IActionResult Put(string id, [FromForm] int mapId, string roomName, string roomPass, string desc)
        {
            Room roomTemp = new Room();
            using (var context = new PixelTownContext())
            {
                roomTemp = context.Room.Where(s => s.Id.Equals(id)).SingleOrDefault();
            }

            var claimUser = User.FindFirst(ClaimTypes.Name).Value;

            var message = "";
            if (mapId == 0)
            {
                message = "Please select map!";
            }
            else if (roomName == "" || roomName == null)
            {
                message = "Please enter room name!";
            }
            else if (roomPass == "" || roomPass == null)
            {
                message = "Please enter room password";
            }
            else if (roomTemp.UserId != claimUser)
            {
                message = "You do not have permission to edit this room!";
            }

            if (message == "")
            {
                var room = RoomRes.updateRoom(id, mapId, roomName, roomPass, desc);
                if (room != null)
                {
                    return Ok(room);
                }
                else
                {
                    return BadRequest(room);
                }
            } else
            {
                return BadRequest(new JsonResult(message));
            }      
        }

        // DELETE api/<RoomController>/5
        [HttpDelete]
        [Authorize]
        [Route("api/room")]
        public IActionResult Delete([FromBody] Room room)
        {
            Room roomTemp = new Room();
            using (var context = new PixelTownContext())
            {
                roomTemp = context.Room.Where(s => s.Id.Equals(room.Id)).SingleOrDefault();
            }

            var claimUser = User.FindFirst(ClaimTypes.Name).Value;

            var message = "";
            if (roomTemp.UserId != claimUser)
            {
                message = "You do not have permission to delete this room!";
            }

            if (message == "")
            {
                var result = RoomRes.deleteRoom(room.Id);
                if (result)
                {
                    return Ok(new JsonResult(result));
                }
                else
                {
                    return BadRequest(new JsonResult(result));
                }
            } else
            {
                return BadRequest(new JsonResult(message));
            }
        }
    }
}
