using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitService;

namespace dotnet.highloadcup.one.Controllers
{
    [Route("visits")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitProvider _visitProvider;

        public VisitsController(IVisitProvider visitProvider)
        {
            _visitProvider = visitProvider;
        }
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
           
                Visit user = _visitProvider.GetById(id);
                if (user.Id == 0)
                {
                    return new JsonResult(new object()) { StatusCode = 404 };
                }
                return new JsonResult(user);
          
        }
    }
}