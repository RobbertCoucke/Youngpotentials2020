using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YoungpotentialsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplyController : ControllerBase
    {

        public ApplyController() { }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Apply()
        {
            var cv = "cv";
            var bijlage = "bijlage";
            var filePath = "path";
            var emailTo = "emailTo";
            var emailFrom = "emailFrom";
            var message = "message";


            //var folderPath = System.IO.Path.Combine("Resources", (isUser ? "users" : "offers"));
            //var filePath = System.IO.Path.Combine(folderPath, id.ToString());
            //var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            //if (!System.IO.Directory.Exists(fullPath))
            //{
            //    return Ok(null);
            //}

            ////var memory = new MemoryStream();
            ////using (var stream = new FileStream(fullPath, FileMode.Open))
            ////{
            ////    stream.CopyTo(memory);
            ////}
            ////memory.Position = 0;

            //var fullFilePath = System.IO.Path.Combine(fullPath, Directory.GetFiles(fullPath).First());


            //return Ok(fullFilePath);
            return Ok();

        }


    }
}