using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Youngpotentials.Service;
using YoungpotentialsAPI.Models.Requests;
using YoungpotentialsAPI.Models.Responses;

namespace YoungpotentialsAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FavoritesController : Controller
    {

        private IFavoritesService _favoritesService;
        private IMapper _mapper;

        public FavoritesController(IFavoritesService favoritesService, IMapper mapper)
        {
            _favoritesService = favoritesService;
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpGet("user/{id}")]
        public IActionResult GetAllFavoritesFromUserId(int id)
        {
            var favorites = _favoritesService.GetAllFavoritesFromUserId(id);
            var offers = new List<OfferResponse>();
            foreach(var f in favorites)
            {
                offers.Add(_mapper.Map<OfferResponse>(f.Offer));
            }
            return Ok(offers);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult AddFavorite([FromBody]FavoritesRequest model)
        {
            var favorite = _favoritesService.AddFavorite(model.UserId, model.OfferId);
            if (favorite != null)
                return Ok(favorite);
            return BadRequest("failed to add Favorite");
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public IActionResult DeleteFavorite(int id)
        {
            _favoritesService.DeleteFavorite(id);
            return Ok();

        }
    }
}