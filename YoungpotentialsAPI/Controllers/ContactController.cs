using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoungpotentialsAPI.Helpers;
using YoungpotentialsAPI.Models.Requests;

namespace YoungpotentialsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> SendMailAsync([FromBody]  Contact contact)
        {

            var emailService = new EmailService();
            //TODO TO 
            await emailService.sendEmailAsync("ibrahemhajkasem@gmail.com", contact.Email, contact.Subject, contact.Message);
            return Ok();
        }
    }
}