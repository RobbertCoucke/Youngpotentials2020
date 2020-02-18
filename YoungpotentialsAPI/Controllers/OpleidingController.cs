using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Service;
using YoungpotentialsAPI.Models.Requests;
using YoungpotentialsAPI.Models.Responses;

namespace YoungpotentialsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpleidingController : Controller
    {
        private IOpleidingService _opleidingService;
        private IMapper _mapper;

        public OpleidingController(IOpleidingService opleidingService, IMapper mapper)
        {
            _opleidingService = opleidingService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = new List<OpleidingResponse>();
            var opleidingen = _opleidingService.GetAll();
            foreach(var o in opleidingen)
            {
                var model = _mapper.Map<OpleidingResponse>(o);
                result.Add(model);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var opleiding = _opleidingService.GetById(id);
            var model = _mapper.Map<OpleidingResponse>(opleiding);
            return Ok(model);

        }

        [HttpGet("studiegebied/{id}")]
        public IActionResult GetAllByStudiegebiedId(string id)
        {
            var result = new List<OpleidingResponse>();
            var opleidingen = _opleidingService.GetAllByStudiegebied(id);
            foreach(var o in opleidingen)
            {
                var model = _mapper.Map<OpleidingResponse>(o);
                result.Add(model);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] OpleidingRequest opleidingRequest)
        {
            var opleiding = _mapper.Map<Opleiding>(opleidingRequest);
            opleiding.Id = id;
            try
            {
                _opleidingService.Update(opleiding);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] OpleidingRequest opleidingRequest)
        {
            var opleiding = _mapper.Map<Opleiding>(opleidingRequest);

            IEnumerable<Opleiding> opleidingen = _opleidingService.GetAll();

            var item = opleidingen.Last().Id.Substring(1);
            var idFromLastElement = Convert.ToInt32(item);
            opleiding.Id = "o" + (idFromLastElement + 1).ToString();
            _opleidingService.CreateOpleiding(opleiding);

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            _opleidingService.DeleteById(id);
            return Ok();
        }
    }
}