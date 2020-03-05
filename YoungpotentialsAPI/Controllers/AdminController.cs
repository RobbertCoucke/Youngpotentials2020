using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Youngpotentials.Service;

namespace YoungpotentialsAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private ICompanyService _companyService; 

        public AdminController(ICompanyService companyService)
        {
            _companyService = companyService;
        }



        [HttpGet("getUnverified")]
        public IActionResult GetAllUnverifiedCompanies()
        {
            var companies = _companyService.GetAllUnverified();

            return Ok(companies);
        }

        [HttpPut]
        public IActionResult VerifyCompany([FromBody] int id)
        {
            _companyService.Verify(id);
            return Ok();
        }

  


    }
}