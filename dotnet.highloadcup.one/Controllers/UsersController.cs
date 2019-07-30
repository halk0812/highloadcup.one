using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Responce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService;
using VisitService;

namespace dotnet.highloadcup.one.Controllers
{
    [Route("users")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IVisitProvider _visitProvider;
        private readonly IUserProvider _userProvider;
        public UsersController(IUserProvider userProvider, IVisitProvider visitProvider)
        {
            _userProvider = userProvider;
            _visitProvider = visitProvider;
        }
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            User user = await _userProvider.GetByIdAsync(id);
            if (user == null)
            {
                return new JsonResult(new object()) { StatusCode = 404 };
            }
            return new JsonResult(user);
        }
        [HttpGet("{id}/visits")]
        public async Task<JsonResult> Get(int id, [FromQuery] UInt32? fromDate, [FromQuery] UInt32 toDate, [FromQuery] string country, [FromQuery] int? toDistance)
        {
            User user = await _userProvider.GetByIdAsync(id);
            if (user == null)
            {
                return new JsonResult(new object()) { StatusCode = 404 };
            }
            UserVisits visits = await _visitProvider.GetByUserIdWithParametrsAsync(id,fromDate, toDate, country, toDistance);
            if (visits == null)
            {
                return new JsonResult(new object()) { StatusCode = 404 };
            }
            return new JsonResult(visits);
        }
    }
}