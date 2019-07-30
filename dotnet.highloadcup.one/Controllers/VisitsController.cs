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
        public async Task<JsonResult> Get(int id)
        {
            Visit user = await _visitProvider.GetByIdAsync(id);
            if (user == null)
            {
                return new JsonResult(new object()) { StatusCode = 404 };
            }
            return new JsonResult(user);
        }
    }
}