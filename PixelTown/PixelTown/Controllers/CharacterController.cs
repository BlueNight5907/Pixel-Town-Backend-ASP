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
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelTown.Controllers
{
    public class CharacterController : ControllerBase
    {

        public static IHostingEnvironment environment;

        public CharacterController(IHostingEnvironment envi)
        {
            environment = envi;
        }

        // GET: api/<CharacterController>
        [HttpGet]
        [Authorize]
        [Route("api/character")]
        public ActionResult<IEnumerable<Character>> Get()
        {
            var result = CharacterRes.getall();
            return Ok(new { code = "success", value = result });
        }

        // GET api/<CharacterController>/5
        [HttpGet]
        [Authorize]
        [Route("api/character/id")]
        public IActionResult Get([FromBody] Character character)
        {
            var result = CharacterRes.getById(character.Id);
            return Ok(new { code = "success", value = result });
        }

        // POST api/<CharacterController>
        [HttpPost]
        [Authorize]
        [Route("api/character")]
        public async Task<IActionResult> Post([FromForm] string name, IFormFile file1, IFormFile file2)
        {
            var claim = User.Claims.Where(claim => claim.Value.Equals("Admin")).ToList();
            if (claim.Count() > 0)
            {
                var message = "";
                if (name == "" || name == null)
                {
                    message = "Please enter character name!";
                }
                else if (file1 == null)
                {
                    message = "Please choosing image title set!";
                }
                else if (Path.GetFileName(file1.FileName) == "atlas.png")
                {
                    message = "File name must be atlas.png";
                }
                else if (file2 == null)
                {
                    message = "Please choosing json file of character!";
                }
                else if (Path.GetFileName(file2.FileName) == "atlas.json")
                {
                    message = "File name must be asset.json";
                }

                if (message == "")
                {
                    if (file1 != null && file2 != null)
                    {
                        string path = Path.Combine(environment.WebRootPath, "public\\characters\\" + name);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        var environ = environment.WebRootPath;
                        var fileName = Path.Combine(environ, "public\\characters\\" + name + "\\", Path.GetFileName(file1.FileName));
                        var filePath = Path.Combine(environ, "public\\characters\\" + name + "\\", Path.GetFileName(file1.FileName));
                        string sub = filePath.Substring(0, filePath.Length - Path.GetFileName(file1.FileName).Length);
                        string ext = Path.GetExtension(fileName);
                        string fileNameOnly = Path.Combine(sub, Path.GetFileNameWithoutExtension(fileName));

                        var fileName1 = Path.Combine(environ, "public\\characters\\" + name + "\\", Path.GetFileName(file2.FileName));
                        var filePath1 = Path.Combine(environ, "public\\characters\\" + name + "\\", Path.GetFileName(file2.FileName));
                        string sub1 = filePath.Substring(0, filePath1.Length - Path.GetFileName(file2.FileName).Length);
                        string ext1 = Path.GetExtension(fileName1);
                        string fileNameOnly1 = Path.Combine(sub1, Path.GetFileNameWithoutExtension(fileName1));

                        int i = 0;
                        while (System.IO.File.Exists(fileName))
                        {
                            i += 1;
                            fileName = string.Format("{0}({1}){2}", fileNameOnly, i, ext);
                        }

                        int i1 = 0;
                        while (System.IO.File.Exists(fileName1))
                        {
                            i1 += 1;
                            fileName1 = string.Format("{0}({1}){2}", fileNameOnly1, i1, ext1);
                        }

                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            await file1.CopyToAsync(fileStream);
                        }

                        using (var fileStream = new FileStream(fileName1, FileMode.Create))
                        {
                            await file2.CopyToAsync(fileStream);
                        }

                        var result = CharacterRes.createCharacter(name);
                        if (result != null)
                        {
                            return Ok(new { code = "success", value = result });
                        }
                        else
                        {
                            return BadRequest(new { code = "error", value = result });
                        }
                    }
                    else
                    {
                        return Problem(
                            detail: "File 1 and File 2 not null",
                            statusCode: StatusCodes.Status404NotFound,
                            instance: HttpContext.Request.Path
                        );
                    }
                }
                else
                {
                    return BadRequest(new JsonResult(message));
                }
            } else
            {
                return Problem(
                    detail: "Bạn không phải admin",
                    statusCode: StatusCodes.Status403Forbidden,
                    instance: HttpContext.Request.Path
                );
            }          
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete]
        [Authorize]
        [Route("api/character")]
        public IActionResult Delete([FromBody] Character character)
        {
            var claim = User.Claims.Where(claim => claim.Value.Equals("Admin")).ToList();
            if (claim.Count() > 0)
            {
                var temp = CharacterRes.getById(character.Id);

                string path = Path.Combine(environment.WebRootPath, "public\\characters\\" + temp.CharacterName);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }

                var result = CharacterRes.deleteCharacter(character.Id);
                if (result)
                {
                    return Ok(new { code = "success", value = result });
                }
                else
                {
                    return BadRequest(new { code = "error", value = result });
                }
            } else
            {
                return Problem(
                    detail: "Bạn không phải admin",
                    statusCode: StatusCodes.Status403Forbidden,
                    instance: HttpContext.Request.Path
                );
            }        
        }
    }
}
