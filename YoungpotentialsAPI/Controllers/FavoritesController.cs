using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Youngpotentials.Domain.Models.Responses;
using Youngpotentials.Service;
using YoungpotentialsAPI.Models.Requests;
using YoungpotentialsAPI.Models.Responses;

namespace YoungpotentialsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FavoritesController : Controller
    {

        private IFavoritesService _favoritesService;
        private IMapper _mapper;
        private IStudentService _studentService;

        public FavoritesController(IFavoritesService favoritesService, IMapper mapper, IStudentService studentService)
        {
            _favoritesService = favoritesService;
            _mapper = mapper;
            _studentService = studentService;
        }

        [Authorize(Roles = "User")]
        [HttpGet("user/{id}")]
        public IActionResult GetAllFavoritesFromUserId(int id)
        {

            var student = _studentService.GetStudentByUserId(id);
            var favorites = _favoritesService.GetAllFavoritesFromUserId(student.Id);
            var result = new List<FavoriteResponse>();
            foreach(var f in favorites)
            {
                result.Add(new FavoriteResponse
                {
                    Id = f.Id,
                    Vacature = _mapper.Map<OfferResponse>(f.Offer)

                }) ;
            }
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult AddFavorite([FromBody]FavoritesRequest model)
        {
            var student = _studentService.GetStudentByUserId(model.UserId);
            var favorite = _favoritesService.AddFavorite(student.Id, model.OfferId);
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