using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelTown.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PixelTown.Controllers
{
    public class MessageController : Controller
    {
        public static IHostingEnvironment environment;

        public MessageController(IHostingEnvironment envi)
        {
            environment = envi;
        }
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

        // GET: api/<MapController>
        [HttpGet]
        [Authorize]
        [Route("api/files/{roomId}/{time}")]
        public ActionResult<IEnumerable<object>> GetFile(string roomId, long time)
        {
            var userId = User.FindFirst(ClaimTypes.Name).Value;
            Console.WriteLine(roomId + " " + userId);
            if (RoomRes.checkUserInRoom(roomId, userId))
            {
                var result = MessageRes.GetFiles(roomId, time);
                return Ok(new JsonResult(result));
            }
            else
            {
                var message = "You're not in this room!!!";
                return BadRequest(new JsonResult(message));
            }
        }

        // GET: api/<MapController>
        [HttpGet]
        [Authorize]
        [Route("api/files/last/{roomId}/{time}")]
        public ActionResult<IEnumerable<object>> GetFileLast(string roomId, long time)
        {
            var userId = User.FindFirst(ClaimTypes.Name).Value;
            Console.WriteLine(roomId + " " + userId);
            if (RoomRes.checkUserInRoom(roomId, userId))
            {
                var result = MessageRes.GetLastFile(roomId,time);
                return Ok(new JsonResult(result));
            }
            else
            {
                var message = "You're not in this room!!!";
                return BadRequest(new JsonResult(message));
            }
        }
        // PUT api/<AccountController>/5
        [HttpPost]
        [Authorize]
        [Route("api/files/{roomId}")]
        public async Task<IActionResult> Upload(string roomId, [FromForm] IFormFile file)
        {
            var claimUser = User.FindFirst(ClaimTypes.Name).Value;
            if (file != null && roomId != null)
            {
                var environ = environment.WebRootPath;
                var fileName = Path.Combine(environ, "public\\files\\"+roomId+"\\", Path.GetFileName(file.FileName));
                string sub = fileName.Substring(0, fileName.Length - Path.GetFileName(file.FileName).Length);
                string ext = Path.GetExtension(fileName);
                string fileNameOnly = Path.Combine(sub, Path.GetFileNameWithoutExtension(fileName));
                int i = 0;
                var fileDatabase = Path.GetFileName(file.FileName);
                if (!Directory.Exists(Path.Combine(environ, "public\\files\\" + roomId)))
                {
                    Directory.CreateDirectory(Path.Combine(environ, "public\\files\\" + roomId));
                }   
                while (System.IO.File.Exists(fileName))
                {
                    i += 1;
                    fileName = string.Format("{0}({1}){2}", fileNameOnly, i, ext);
                    fileDatabase = string.Format("{0}({1}){2}", Path.GetFileNameWithoutExtension(file.FileName), i, ext);
                }
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var fileInfor = MessageRes.AddFile(roomId, claimUser, "/public/files/" + roomId + "/" + fileDatabase);
                if (fileInfor != null)
                {
                    return Ok(new JsonResult(fileInfor));
                }
                else
                {
                    return BadRequest("An Error has occured!!");
                }
            }
            return BadRequest("No file has send!!!");
        }
    }
}
