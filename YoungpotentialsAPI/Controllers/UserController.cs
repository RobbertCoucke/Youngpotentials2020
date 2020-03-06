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
using Youngpotentials.Domain.Models.Responses;
using Youngpotentials.Domain.Models.Requests;
using Youngpotentials.Service;
using AutoMapper;
using YoungpotentialsAPI.Helpers;
using Youngpotentials.Domain.Entities;
using Microsoft.AspNetCore.Cors;

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
        private EmailService _mailService = new EmailService();

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
                Id = user.Id,
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


        //[HttpGet("password/{email}")]
        //public async void ResetEmail(string email)
        //{
        //    var user = _userService.GetUserByEmail(email);
        //    if (user != null)
        //    {
        //        var body = "klik op deze link om een nieuw passwoord in te stellen: Click <a href=\"http://myAngularSite/passwordReset?code= " + user.Code +"\>here</a>";
        //        await _mailService.sendEmailAsync(email, "testEmail", "password reset", body);

        //    }
        //}

        //[HttpPost("password")]
        //public IActionResult PasswordReset([FromBody] string req)
        //{
        //    var user = _userService.GetByCode(req.code);
        //    var result =_userService.ResetPassword(user, req.password);
        //    return Ok();

        //}

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
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role.Name,
                    Token = tokenString
                });


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

        [HttpPost("password")]
        public IActionResult ForgotPassword(string email)
        {
            var user = _userService.GetUserByEmail(email);
            return Ok();


        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            UserResponse model = null;
            if(user != null)
            {
               var userResponse = _mapper.Map<UserResponse>(user);
               var roleId = user.RoleId;
               //user is student
               if(roleId == 2)
                {
                    var student = _studentService.GetStudentByUserId(user.Id);
                    if(student != null)
                    {
                        
                        
                        model = _mapper.Map<StudentResponse>(student);
                        model.Address = student.User.Email;
                        model.Email = student.User.Email;
                        model.City = student.User.City;
                        model.Telephone = student.User.Telephone;
                        model.ZipCode = student.User.ZipCode;
                        model.IsStudent = true;
                    }

                }
                else if( roleId == 3)
                {
                    var company = _companyService.GetCompanyByUserId(user.Id);
                    if(company != null)
                    {
                        model = _mapper.Map<CompanyResponse>(company); 
                        model.Address = company.User.Email;
                        model.City = company.User.City;
                        model.Telephone = company.User.Telephone;
                        model.ZipCode = company.User.ZipCode;
                        model.IsStudent = false;
                    }

                }
            }

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