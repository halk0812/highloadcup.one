using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.highloadcup.one.Controllers
{
    [Route("visits")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok();
        }
    }
}