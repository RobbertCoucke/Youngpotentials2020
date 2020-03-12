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
    public class SectorController : ControllerBase
    {

        private ISectorService _sectorService;
        private IMapper _mapper;

        public SectorController(IMapper mapper, ISectorService sectorService)
        {
            _mapper = mapper;
            _sectorService = sectorService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var sectors = _sectorService.GetAll();
            var result = new List<SectorResponse>();
            foreach(var sector in sectors)
            {
                result.Add(_mapper.Map<SectorResponse>(sector));
            }

            return Ok(result);
        }

    }
}