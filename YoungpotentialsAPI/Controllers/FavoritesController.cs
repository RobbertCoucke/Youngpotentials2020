using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YoungpotentialsAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class FavoritesController : Controller
    {

        //private IFavoritesService _favoritesService;

        //public FavoritesController(IFavoritesService favoritesService)
        //{
        //    _favoritesService = favoritesService;
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}