using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YoungpotentialsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : Controller
    {

        //private IOfferService _offerService;

        //public OfferController(IOfferService offerService)
        //{
        //    _offerService = offerService;
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}