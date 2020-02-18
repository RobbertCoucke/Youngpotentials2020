using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Service;
using YoungpotentialsAPI.Helpers;
using YoungpotentialsAPI.Models.Responses;

namespace YoungpotentialsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudiegebiedController : Controller
    {
        private IStudiegebiedService _studiegebiedService;
        private IMapper _mapper;

        public StudiegebiedController(IStudiegebiedService studiegebiedService, IMapper mapper )
        {
            _studiegebiedService = studiegebiedService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = new List<StudiegebiedResponse>();
            var studiegebieds = _studiegebiedService.GetAll();
            foreach(var s in studiegebieds)
            {
                var model = _mapper.Map<StudiegebiedResponse>(s);
                result.Add(model);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var studieGebied = _studiegebiedService.GetById(id);
            var model = _mapper.Map<StudiegebiedResponseDetail>(studieGebied);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id,[FromBody] StudiegebiedRequest studiegebiedRequest)
        {
            var studiegebied = _mapper.Map<Studiegebied>(studiegebiedRequest);
            studiegebied.Id = id;
            try
            {
                _studiegebiedService.UpdateStudegebied(studiegebied);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult create([FromBody] StudiegebiedRequest studiegebiedRequest)
        {
            var studiegebied = _mapper.Map<Studiegebied>(studiegebiedRequest);

            IEnumerable<Studiegebied> studiegebieds = _studiegebiedService.GetAll();
            var lastItem = studiegebieds.Last().Id.Substring(1);
            var idFromLastElement = Convert.ToInt32(lastItem);
            studiegebied.Id = "s" + ((idFromLastElement + 1).ToString());
            _studiegebiedService.CreateStudiegebied(studiegebied);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteById(string id)
        {
             _studiegebiedService.DeleteStudieGebied(id);
            return Ok();
        }
        
    }
}