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
    [Route("api/map")]
    public class MapController : ControllerBase
    {
        // GET: api/<MapController>
        [HttpGet]
        [Authorize]
        [Route("all")]
        public ActionResult<IEnumerable<Map>> Get()
        {
            var result = MapRes.getall();
            return Ok(new JsonResult(result));
        }

        // GET api/<MapController>/5
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromBody] Map map)
        {
            var result = MapRes.getById(map.Id);
            return Ok(new JsonResult(result));
        }
    }
}
