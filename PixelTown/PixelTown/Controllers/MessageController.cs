using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixelTown.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PixelTown.Controllers
{
    public class MessageController : Controller
    {
        // GET: api/<MapController>
        [HttpGet]
        [Authorize]
        [Route("api/messages/{roomId}/{time}")]
        public ActionResult<IEnumerable<object>> GetMessage(string roomId, long time)
        {
            var userId = User.FindFirst(ClaimTypes.Name).Value;
            Console.WriteLine(roomId + " " + userId);
            if (RoomRes.checkUserInRoom(roomId,userId))
            {
                var result = MessageRes.GetMessages(roomId, time);
                return Ok(new JsonResult(result));
            }
            else{
                var message = "You're not in this room!!!";
                return BadRequest(new JsonResult(message));
            } 
        }
    }
}
