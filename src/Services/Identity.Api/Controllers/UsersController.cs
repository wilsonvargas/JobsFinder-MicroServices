using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Api.Models;
using Identity.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityRepository _identityRespository;

        public UsersController(IIdentityRepository identityRespository)
        {
            _identityRespository = identityRespository;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _identityRespository.GetUser(id);
            return Ok(user);
        }

        // GET api/users/applicationcount/5
        [HttpGet("applicationcount/{id}")]
        public async Task<IActionResult> GetUserApplicantCount(string id)
        {
            var count = await _identityRespository.GetUserApplicationCount(id);
            return Ok(count);
        }


        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            var user = await _identityRespository.UpdateUser(value);
            return Ok(user);
        }
    }
}
