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
    public class KeuzeController : Controller
    {
            private IKeuzeService _keuzeService;
            private IMapper _mapper;

            public KeuzeController(IKeuzeService keuzeService, IMapper mapper)
            {
                _keuzeService = keuzeService;
                _mapper = mapper;
            }

            [HttpGet]
            public IActionResult Index()
            {
                var result = new List<KeuzeResponse>();
                var keuzes = _keuzeService.GetAll();
                foreach (var k in keuzes)
                {
                    var model = _mapper.Map<KeuzeResponse>(k);
                    result.Add(model);
                }

                return Ok(result);
            }

            [HttpGet("{id}")]
            public IActionResult GetById(string id)
            {
                var keuze = _keuzeService.GetById(id);
                var model = _mapper.Map<KeuzeResponse>(keuze);
                return Ok(model);
            }

            [HttpPut("{id}")]
            public IActionResult Update(string id, [FromBody] KeuzeRequest keuzeRequest)
            {
                var keuze = _mapper.Map<Keuze>(keuzeRequest);
                keuze.Id = id;
                try
                {
                    _keuzeService.Update(keuze);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(new { message = e.Message });
                }
            }

            [HttpPost]
            public IActionResult create([FromBody] KeuzeRequest keuzeRequest)
            {
                var keuze = _mapper.Map<Keuze>(keuzeRequest);

                IEnumerable<Keuze> keuzes = _keuzeService.GetAll();
                var lastItem = keuzes.Last().Id.Substring(1);
                var idFromLastElement = Convert.ToInt32(lastItem);
                keuze.Id = "k" + ((idFromLastElement + 1).ToString());
                _keuzeService.CreateKeuze(keuze);
                return Ok();
            }

            [HttpDelete("{id}")]
            public IActionResult deleteById(string id)
            {
                _keuzeService.DeleteById(id);
                return Ok();
            }
        }
}