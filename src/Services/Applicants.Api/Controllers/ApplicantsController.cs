﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applicants.Api.Models;
using Applicants.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Applicants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantsController(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Applicant>> Get()
        {
            return await _applicantRepository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
