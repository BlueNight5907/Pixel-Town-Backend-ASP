using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixelTown.Models;
using PixelTown.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelTown.Controllers
{
    public class SearchController : ControllerBase
    {
        // GET: api/<SearchController>
        [HttpGet]
        [Authorize]
        [Route("api/search")]
        public ActionResult<IEnumerable<Room>> Get([FromForm] string key)
        {
            var result = SearchRes.search(key);
            return Ok(new JsonResult(result));
        }
    }
}
