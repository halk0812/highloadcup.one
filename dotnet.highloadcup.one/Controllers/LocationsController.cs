using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using LocationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.highloadcup.one.Controllers
{
    [Route("locations")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationProvider _locationProvider;
        public LocationsController(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }
        [HttpGet("{id}")]
        public JsonResult Get(uint id)
        {
            
                Location user = _locationProvider.GetById(id);
                if (user.Id == 0)
                {
                    return new JsonResult(new object()) { StatusCode = 404 };
                }
                return new JsonResult(user);
          
        }
        [HttpGet("{id}/avg")]
        public JsonResult Get(uint id, [FromQuery] UInt32? fromDate, [FromQuery] UInt32? toDate, [FromQuery] int? fromAge, [FromQuery] int? toAge, [FromBody] string gender )
        {
           
                Location user =_locationProvider.GetById(id);
                if (user.Id == 0)
                {
                    return new JsonResult(new object()) { StatusCode = 404 };
                }
                return new JsonResult(user);
         
        }
    }
}