using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using YoungpotentialsAPI.Models.Responses;
using YoungpotentialsAPI.Models.Requests;
using Youngpotentials.Service;
using AutoMapper;
using YoungpotentialsAPI.Helpers;
using Youngpotentials.Domain.Entities;

namespace YoungpotentialsAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IStudentService _studentService;
        private IRoleService _roleService;
        private ICompanyService _companyService;
        private readonly AppSettings _appSettings;
        private IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings, IStudentService studentService, ICompanyService companyService, IRoleService roleService)
        {
            _userService = userService;
            _companyService = companyService;
            _studentService = studentService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "email or password is incorrect" });
            }

            var role = user.Role.Name;
      
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new Claim(ClaimTypes.Role, "Admin");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    //ipv "Admin" role ophalen van user
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new AuthenticationResponse
            {
                Email = user.Email,
                Role = user.Role.Name,
                Token = tokenString
            }) ;

        }

        [Authorize(Roles = "User")]
        [HttpGet("test")]
        public JsonResult Test()
        {
            return Json("het werk woehoe!");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserRegistrationRequest model)
        {
            var user = _mapper.Map<AspNetUsers>(model);

            try {

                if (model.IsStudent)
                    user.RoleId = _roleService.GetRoleByName("User").Id;
                else
                    user.RoleId = _roleService.GetRoleByName("Company").Id;

                user = _userService.Create(user, model.Password);

                

                if (model.IsStudent)
                {
                    var student = _mapper.Map<Students>(model);
                    student.UserId = user.Id;
                    _studentService.CreateStudent(student);
                
                }
                else
                {
                    var company = _mapper.Map<Companies>(model);
                    company.UserId = user.Id;
                    _companyService.CreateCompany(company);
                }


                return Ok();


            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = new List<UserResponse>();
            var users = _userService.GetAll();
            foreach(var user in users)
            {
                var model = _mapper.Map<UserResponse>(user);
                result.Add(model);
            }
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var model = _mapper.Map<UserResponse>(user);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserUpdateRequest model)
        {

            var user = _mapper.Map<AspNetUsers>(model);
            user.Id = id;
            try
            {
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

    }
}