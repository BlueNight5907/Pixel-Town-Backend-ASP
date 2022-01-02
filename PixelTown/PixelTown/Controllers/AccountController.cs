using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelTown.Models;
using PixelTown.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelTown.Controllers
{
    [Route("api/account")]
    //[Authorize]//(Roles = "Admin")]
    public class AccountController : ControllerBase
    {
        public static IHostingEnvironment environment;

        public AccountController(IHostingEnvironment envi)
        {
            environment = envi;
        }

        // GET: api/<AccountController>
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Account>> Get()
        {
            var claim = User.Claims.Where(claim => claim.Value.Equals("Admin")).ToList();   
            if (claim.Count() > 0)
            {
                var lstAccount = AccountRes.get();
                return new JsonResult(lstAccount);
            }
            else
            {
                return Problem(
                    detail: "Bạn không phải admin",
                    statusCode: StatusCodes.Status403Forbidden,
                    instance: HttpContext.Request.Path
                );
            }
        }

        // GET api/<AccountController>/5
        // admin
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Account> GetbyId(string id)
        {
            var claim = User.Claims.Where(claim => claim.Value.Equals("Admin")).ToList();
            if (claim.Count() > 0)
            {
                var account = AccountRes.getById(id);
                if (account == null)
                {
                    return BadRequest(new JsonResult(account));
                }
                else
                {
                    return account;
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

        [HttpGet]
        [Route("token")]
        [Authorize]
        public ActionResult<Account> GetbyToken()
        {
            var claimUser = User.FindFirst(ClaimTypes.Name).Value;
            var account = AccountRes.getById(claimUser);
            if (account == null)
            {
                return BadRequest(new JsonResult(account));
            }
            else
            {
                return Ok(new { id = account.Id, name = account.Name, email = account.Email,  birthday = account.Birthday, address = account.Address, avatar = account.Avatar });
            }
        }

        [HttpGet("user/{id}")]
        public ActionResult<Account> GetbyUserId(string id)
        {
            var account = AccountRes.getById(id);
            if (account == null)
            {
                return BadRequest(new JsonResult(account));
            }
            else
            {
                return Ok(new { id = account.Id, name = account.Name, avatar = account.Avatar });
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromForm] string name, string birthday, string address, IFormFile image)
        {
            var message = "";
            if (name == "" || name == null)
            {
                message = "Please enter full name!";
            } else if (address == "" || address == null)
            {
                message = "Please enter address!";
            }

            if (message == "")
            {
                var claimUser = User.FindFirst(ClaimTypes.Name).Value;
                if (image != null)
                {
                    var environ = environment.WebRootPath;
                    var fileName = Path.Combine(environ, "public\\users\\", Path.GetFileName(image.FileName));
                    var filePath = Path.Combine(environ, "public\\users\\", Path.GetFileName(image.FileName));
                    string sub = filePath.Substring(0, filePath.Length - Path.GetFileName(image.FileName).Length);
                    string ext = Path.GetExtension(fileName);
                    string fileNameOnly = Path.Combine(sub, Path.GetFileNameWithoutExtension(fileName));
                    int i = 0;
                    var fileDatabase = Path.GetFileName(image.FileName);
                    while (System.IO.File.Exists(fileName))
                    {
                        i += 1;
                        fileName = string.Format("{0}({1}){2}", fileNameOnly, i, ext);
                        fileDatabase = string.Format("{0}({1}){2}", Path.GetFileNameWithoutExtension(image.FileName), i, ext);
                    }
                    using (var fileStream = new FileStream(fileName, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    DateTime date = DateTime.ParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var account = AccountRes.updateAccount(claimUser, name, date, address, "/public/users/" + fileDatabase);
                    if (account != null)
                    {
                        return Ok(new JsonResult(account));
                    }
                    else
                    {
                        return BadRequest(new JsonResult(account));
                    }
                }
                else
                {
                    DateTime date = DateTime.ParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var account = AccountRes.updateAccount(claimUser, name, date, address, null);
                    if (account != null)
                    {
                        return Ok(new JsonResult(account));
                    }
                    else
                    {
                        return BadRequest(new JsonResult(account));
                    }
                }
            } else
            {
                return BadRequest(new JsonResult(message));
            }
            
        }

        [HttpPut]
        [Authorize]
        [Route("admin")]
        public async Task<IActionResult> PutAdmin([FromForm] string id, string name, string password, string birthday, string address, IFormFile image)
        {
            var claim = User.Claims.Where(claim => claim.Value.Equals("Admin")).ToList();
            if (claim.Count() > 0)
            {
                var message = "";
                if (name == "" || name == null)
                {
                    message = "Please enter full name!";
                } else if (password == "" || password == null)
                {
                    message = "Please enter password!";
                } else if (password.Length < 6)
                {
                    message = "Password not less than 6 characters!";
                }
                else if (address == "" || address == null)
                {
                    message = "Please enter address!";
                }
                if (message == "")
                {
                    if (image != null)
                    {
                        var environ = environment.WebRootPath;
                        var fileName = Path.Combine(environ, "public\\users\\", Path.GetFileName(image.FileName));
                        var filePath = Path.Combine(environ, "public\\users\\", Path.GetFileName(image.FileName));
                        string sub = filePath.Substring(0, filePath.Length - Path.GetFileName(image.FileName).Length);
                        string ext = Path.GetExtension(fileName);
                        string fileNameOnly = Path.Combine(sub, Path.GetFileNameWithoutExtension(fileName));
                        int i = 0;
                        var fileDatabase = Path.GetFileName(image.FileName);
                        while (System.IO.File.Exists(fileName))
                        {
                            i += 1;
                            fileName = string.Format("{0}({1}){2}", fileNameOnly, i, ext);
                            fileDatabase = string.Format("{0}({1}){2}", Path.GetFileNameWithoutExtension(image.FileName), i, ext);
                        }
                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        DateTime date = DateTime.ParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var account = AccountRes.AdminupdateAccount(id, name, password, date, address, "/public/users/" + fileDatabase);
                        if (account != null)
                        {
                            return Ok(new JsonResult(account));
                        }
                        else
                        {
                            return BadRequest(new JsonResult(account));
                        }
                    }
                    else
                    {
                        DateTime date = DateTime.ParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var account = AccountRes.AdminupdateAccount(id, name, password, date, address, null);
                        if (account != null)
                        {
                            return Ok(new JsonResult(account));
                        }
                        else
                        {
                            return BadRequest(new JsonResult(account));
                        }
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

        // DELETE api/<AccountController>/5
        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromBody] Account acc)
        {
            var claim = User.Claims.Where(claim => claim.Value.Equals("Admin")).ToList();
            if (claim.Count() > 0)
            {
                var result = AccountRes.deleteAccount(acc.Id);
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
                return Problem(
                    detail: "Bạn không phải admin",
                    statusCode: StatusCodes.Status403Forbidden,
                    instance: HttpContext.Request.Path
                );
            }
        }

        [HttpPut]
        [Authorize]
        [Route("changepassword")]
        public IActionResult ChangePass([FromBody] changePassword newPass)
        {
            var message = "";
            var claimUser = User.FindFirst(ClaimTypes.Name).Value;
            Account acc = new Account();
            using (var context = new PixelTownContext())
            {
                acc = context.Account.Where(s => s.Id.Equals(claimUser)).SingleOrDefault();
            }
            bool hashPassword = BCrypt.Net.BCrypt.Verify(newPass.oldPassword, acc.Password);
            if (newPass.oldPassword == "" || newPass.oldPassword == null)
            {
                message = "Please enter old password!";
            } else if (hashPassword == false)
            {
                message = "Old password incorrect. Please enter again!";
            } else if (newPass.Password == "" || newPass.Password == null)
            {
                message = "Please enter new password!";
            }
            else if (newPass.Password.Length < 6)
            {
                message = "Password not less than 6 characters!";
            }
            if (message == "")
            {
                var result = AccountRes.changePassword(claimUser, newPass.oldPassword, newPass.Password);
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
