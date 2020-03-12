using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Youngpotentials.Domain.Models.Responses;
using Youngpotentials.Service;

namespace YoungpotentialsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private ICompanyService _companyService;
        private IMapper _mapper;
        private IOfferService _offerService;
        public CompanyController(ICompanyService companyService, IMapper mapper, IOfferService offerService)
        {
            _companyService = companyService;
            _mapper = mapper;
            _offerService = offerService;
        }

        [HttpGet("unverified")]
        public IActionResult GetAllUnverifiedCompanies()
        {

            var companies = _companyService.GetAllUnverified();
            var result = new List<CompanyResponse>();
            foreach(var company in companies)
            {
                
                var model = _mapper.Map<CompanyResponse>(company);
                model.Email = company.User.Email;
                model.Address = company.User.Address;
                model.Sector = company.Sector;
                model.City = company.User.City;
                model.Telephone = company.User.Telephone;
                model.ZipCode = company.User.ZipCode;
                model.IsStudent = false;
                //fix circle issue
                if(model.Sector != null)
                    model.Sector.Companies = null;
                result.Add(model);
            }

            return Ok(result);

            
        }

        [HttpGet("verified")]
        public IActionResult GetAllVerifiedCompanies()
        {

         
            var companies = _companyService.GetAllVerified();
            var result = new List<CompanyResponse>();
            foreach (var company in companies)
            {

                var model = _mapper.Map<CompanyResponse>(company);
                model.Address = company.User.Address;
                model.Email = company.User.Email;
                model.Sector = company.Sector;
                model.City = company.User.City;
                model.Telephone = company.User.Telephone;
                model.ZipCode = company.User.ZipCode;
                model.IsStudent = false;
                //fix circle issue
                if (model.Sector != null)
                    model.Sector.Companies = null;
                result.Add(model);
            }

            return Ok(result);


        }



        [HttpGet("verify/{companyId}")]
        public IActionResult VerifyCompany(int companyId)
        {
            var company = _companyService.GetCompanyById(companyId);
            company.Verified = true;
            _companyService.UpdateCompany(company);
            
            return Ok();
        }

        [HttpGet("unverify/{companyId}")]
        public IActionResult UnVerifyCompany( int companyId)
        {
            var company = _companyService.GetCompanyById(companyId);
            company.Verified = false;
            _companyService.UpdateCompany(company);
            return Ok();
        }

    }
}