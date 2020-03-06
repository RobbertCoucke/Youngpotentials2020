using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Service;
using Youngpotentials.Domain.Models.Requests;
using Youngpotentials.Domain.Models.Responses;

namespace YoungpotentialsAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OfferController : Controller
    {
        private IOfferService _offerService;
        private IUserService _userService;
        private ICompanyService _companyService;
        private IMapper _mapper;

        public OfferController(IOfferService offerService, IUserService userService, IMapper mapper,ICompanyService companyService)
        {
            _offerService = offerService;
            _userService = userService;
            _mapper = mapper;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("getAll")]
        public IActionResult GetAllOffers()
        {
            var offers = _offerService.GetAllOffers();
            foreach(var offer in offers)
            {
                offer.OpleidingOffer = new List<OpleidingOffer>();
                offer.StudiegebiedOffer = new List<StudiegebiedOffer>();
            }

            //offersResponse.Add(_mapper.Map<OfferResponse>(offer));

            return Ok(offers);
        }

        [HttpGet("getAllCompany/{id}")]
        public IActionResult GetAllOffersByCompany(int id)
        {
            var offers = _offerService.GetAllOffersByCompany(id);
            var offerResponses = new List<OfferResponse>();
            foreach (var offer in offers)
            {
                var offerResponse = _mapper.Map<OfferResponse>(offer);
                offerResponses.Add(offerResponse);
            }
            return Ok(offerResponses);
        }


        //[HttpPost]
        //public IActionResult CreateOffer([FromBody]CreateOfferRequest model)
        //{
        //    var offer = _mapper.Map<Offers>(model);
        //    var company = _companyService.GetCompanyById(model.CompanyId);
        //    if (company.Verified)
        //    {
        //        offer.Verified = true;
        //    }
        //    else
        //    {
        //        offer.Verified = false;
        //    }

        //    var result = _offerService.CreateOffer(offer);
        //    if(model.tags != null)
        //    {
        //        _offerService.AddTagsToOffer(model.tags, result.Id);
        //    }
        //    return Ok(result);
        //}

        [HttpPut("{id}")]
        public IActionResult UpdateOffer([FromBody]UpdateOfferRequest model, int id)
        {
            _offerService.UpdateOffer(model, id);
            return Ok();
        }

        [HttpGet("types")]
        public IActionResult GetAllTypes()
        {
            var types =_offerService.GetAllTypes();
            return Ok(types);
        }

        [HttpPost("filter")]
        public IActionResult GetOffersByTypesAndTags([FromBody]GetOffersByTypesAndTagsRequest model)
        {
            var offers = _offerService.GetOffersByTypesAndTags(model.types, model.ids);
            var offersResponse = new List<OfferResponse>();
            foreach (var offer in offers)
            {
                var offerResponse = _mapper.Map<OfferResponse>(offer);
                offersResponse.Add(offerResponse);
            }

            return Ok(offersResponse);
        }

        [HttpGet("{id}")]
    public IActionResult GetOfferById(int id)
    {
            var offer = _offerService.GetOfferById(id);
            var offerResponse = _mapper.Map<OfferResponse>(offer);
            return Ok(offerResponse);
    }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _offerService.DeleteOffer(id);
            return Ok();
        }
    }
}