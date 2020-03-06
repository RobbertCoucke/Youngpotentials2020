using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoungpotentialsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : Controller
    {
        // GET: /<controller>/
        
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("file/{isUser}/{id}")]
        public IActionResult GetFile(bool isUser, int id)
        {
            var folderPath = System.IO.Path.Combine("Resources", (isUser ? "users" : "offers"));
            var filePath = System.IO.Path.Combine(folderPath, id.ToString());
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (!System.IO.Directory.Exists(fullPath))
            {
                return Ok(null);
            }

            //var memory = new MemoryStream();
            //using (var stream = new FileStream(fullPath, FileMode.Open))
            //{
            //    stream.CopyTo(memory);
            //}
            //memory.Position = 0;

            var fullFilePath = System.IO.Path.Combine(fullPath,Directory.GetFiles(fullPath).First());


            return Ok(fullFilePath);

        }

        [HttpGet("download/{isUser}/{id}")]
        public IActionResult Download(bool isUser, int id)
        {

            var folderPath = System.IO.Path.Combine("Resources", (isUser ? "users" : "offers"));
            var filePath = System.IO.Path.Combine(folderPath, id.ToString());
            //var fileFullPath = Path.Combine(filePath, fileName);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            var fileName = Directory.GetFiles(fullPath).First();

            var fullFilePath = System.IO.Path.Combine(fullPath, fileName);


            if (!System.IO.File.Exists(fullFilePath))
            {
                return Ok(null);
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(fullFilePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            var file = File(memory, "application/pdf", fileName);

            return file;

        }
 
	
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload( )
        {
            //TODO make sure only one file can be uploaded per UserId/offerId

            try
            {
                var file = Request.Form.Files[0];

                var isUser = Boolean.Parse(Request.Form["isUser"]);
                var id = Int32.Parse(Request.Form["id"]);



                var folderPath = System.IO.Path.Combine("Resources", (isUser ? "users" : "offers" ));
                var filePath = System.IO.Path.Combine(folderPath, id.ToString());
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                Directory.CreateDirectory(pathToSave);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal server error");
            }
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

    }
}
