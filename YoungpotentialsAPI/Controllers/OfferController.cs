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
using Microsoft.AspNetCore.Authorization;
using YoungpotentialsAPI.Helpers;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace YoungpotentialsAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OfferController : Controller
    {
        private IOfferService _offerService;
        private IUserService _userService;
        private ICompanyService _companyService;
        private IFavoritesService _favoritesService;
        private IMapper _mapper;

        public OfferController(IOfferService offerService, IUserService userService, IMapper mapper,ICompanyService companyService, IFavoritesService favoritesService)
        {
            _offerService = offerService;
            _userService = userService;
            _mapper = mapper;
            _companyService = companyService;
            _favoritesService = favoritesService;
        }

        /// <summary>
        /// Get all verified offers that didn't exceed their expiration date
        /// </summary>
        /// <returns>All offers</returns>
        [HttpGet("getAll")]
        public IActionResult GetAllOffers()
        {
            var offers = _offerService.GetAllOffers();
            foreach(var offer in offers)
            {
                offer.OpleidingOffer = new List<OpleidingOffer>();
                offer.StudiegebiedOffer = new List<StudiegebiedOffer>();
                offer.Type = null;
            }

            //offersResponse.Add(_mapper.Map<OfferResponse>(offer));

            return Ok(offers);
        }

        /// <summary>
        /// Get all verified offers of specific company that didn't exceed their expiration date
        /// </summary>
        /// <param name="id">CompanyId</param>
        /// <returns></returns>
        [HttpGet("getAllCompany/{id}")]
        public IActionResult GetAllOffersByCompany(int id)
        {
            var offers = _offerService.GetAllOffersByCompany(id);
            var offerResponses = new List<OfferResponse>();
            foreach (var offer in offers)
            {
                var offerResponse = _mapper.Map<OfferResponse>(offer);
                offerResponses.Add(offerResponse);
                offer.Type = null;
                offer.StudiegebiedOffer = new List<StudiegebiedOffer>();
                offer.OpleidingOffer = new List<OpleidingOffer>();
            }
            return Ok(offerResponses);
        }

        /// <summary>
        /// Creates an offer
        /// </summary>
        /// <param name="model">The request with the information about the offer</param>
        /// <returns>The offer that is created</returns>
        /// 
        [Authorize(Roles = "Company")]
        [HttpPost("create")]
        public IActionResult CreateOffer([FromBody]CreateOfferRequest model)
        {
            try
            {
                var offer = _mapper.Map<Offers>(model);
                var company = _companyService.GetCompanyById(model.CompanyId);
               
                offer.Verified = (bool)company.Verified;
                offer.CompanyName = company.CompanyName;
                offer.Created = DateTime.Now;
                //var types = _offerService.GetAllTypes();
                //offer.Type = types.Where(t => t.Id == offer.TypeId).FirstOrDefault();
                var createdOffer = _offerService.CreateOffer(offer);
                if (model.Tags != null)
                {
                    _offerService.AddTagsToOffer(model.Tags, createdOffer.Id);
                }
                var result = _mapper.Map<OfferResponse>(createdOffer);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Updates an offer
        /// </summary>
        /// <param name="model">The info that got changed from the offer</param>
        /// <param name="id">The offerId that needs to be updated</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateOffer([FromBody]UpdateOfferRequest model, int id)
        {
            _offerService.UpdateOffer(model, id);
            return Ok();
        }

        /// <summary>
        /// Get all types
        /// </summary>
        /// <returns>Alle types</returns>
        [HttpGet("types")]
        public IActionResult GetAllTypes()
        {
            try
            {
                var types = _offerService.GetAllTypes();
                return Ok(types);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Get all offers with the corresponding type and/or tag
        /// </summary>
        /// <param name="model">A list with all types and tags that are selected</param>
        /// <returns>All offers that has 1 or more corresponding types and tags</returns>
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

        /// <summary>
        /// Get an offer by id
        /// </summary>
        /// <param name="id">offerId</param>
        /// <returns>An OfferResponse</returns>
        [HttpGet("{id}")]
        public IActionResult GetOfferById(int id)
        {
            var offer = _offerService.GetOfferById(id);
            var offerResponse = _mapper.Map<OfferResponse>(offer);
            return Ok(offerResponse);
        }

        /// <summary>
        /// Delete an offer by id
        /// </summary>
        /// <param name="id">offerId</param>
        /// <returns>An OK response</returns>
        [Authorize(Roles = "Company")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            /// Delete all the reference before deleting an Offer
            _favoritesService.DeleteAllFavoritesFromOfferId(id);
            _offerService.DeleteAllStudieConnectionsFromOfferId(id);
            
            /// Delete the offer
            _offerService.DeleteOffer(id);
            return Ok();
        }

        /// <summary>
        /// apply for an offer with cv & extra attachement
        /// </summary>
        [Authorize(Roles = "User")]
        [HttpPost("apply")]
        public async void Apply()
        {
            try
            {
                var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

                var emailTo = Request.Form["emailTo"];
                var emailFrom = Request.Form["emailFrom"];
                var subject = Request.Form["subject"];
                var message = Request.Form["message"];
                int? userId = null;
                if (Request.Form["userId"] != "")
                {
                    userId = Int32.Parse( Request.Form["userId"]);
                }
                
                var emailService = new EmailService();
                await emailService.sendEmailWithAttachementAsync(emailTo, emailFrom, subject, message, files, userId);
            }
            catch(Exception e)
            {
                
            }
            
         }
    }
}