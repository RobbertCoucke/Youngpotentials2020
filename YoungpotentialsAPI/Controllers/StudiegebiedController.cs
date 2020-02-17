using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public StudiegebiedController(IUserService userService,
                                      IStudiegebiedService studiegebiedService, IMapper mapper,
                                      IOptions<AppSettings> appSettings)
        {
            _studiegebiedService = studiegebiedService;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("getAll")]
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
    }
}