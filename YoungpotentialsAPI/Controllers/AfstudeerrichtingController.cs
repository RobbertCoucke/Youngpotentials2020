using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Service;
using YoungpotentialsAPI.Models.Responses;

namespace YoungpotentialsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AfstudeerrichtingController : Controller
    {
        private IAfstudeerrichtingService _afstudeerrichtingService;
        private IMapper _mapper;

        public AfstudeerrichtingController(IAfstudeerrichtingService afstudeerrichtingService, IMapper mapper)
        {
            _afstudeerrichtingService = afstudeerrichtingService;
            _mapper = mapper;
        }

        /// <summary>
        /// get all afstudeerrichtingen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var result = new List<AfstudeerrichtingResponse>();
            var afstudeerrichtings = _afstudeerrichtingService.GetAll();
            foreach (var a in afstudeerrichtings)
            {
                var model = _mapper.Map<AfstudeerrichtingResponse>(a);
                result.Add(model);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var afstudeerrichting  = _afstudeerrichtingService.GetById(id);
            var model = _mapper.Map<AfstudeerrichtingResponseDetail>(afstudeerrichting);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] AfstudeerrichtingRequest afstudeerrichtingRequest)
        {
            var afstudeerrichting = _mapper.Map<Afstudeerrichting>(afstudeerrichtingRequest);
            afstudeerrichting.Id = id;
            try
            {
                _afstudeerrichtingService.Update(afstudeerrichting);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult create([FromBody] AfstudeerrichtingRequest afstudeerrichtingRequest)
        {
            var afstudeerrichting = _mapper.Map<Afstudeerrichting>(afstudeerrichtingRequest);

            IEnumerable<Afstudeerrichting> afstudeerrichtings = _afstudeerrichtingService.GetAll();
            var lastItem = afstudeerrichtings.Last().Id.Substring(1);
            var idFromLastElement = Convert.ToInt32(lastItem);
            afstudeerrichting.Id = "a" + ((idFromLastElement + 1).ToString());
            _afstudeerrichtingService.CreateAfstudeerrichting(afstudeerrichting);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteById(string id)
        {
            _afstudeerrichtingService.DeleteById(id);
            return Ok();
        }
    }
}