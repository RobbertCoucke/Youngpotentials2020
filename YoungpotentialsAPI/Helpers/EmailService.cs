using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Helpers
{
    public class EmailService
    {

        public async Task sendEmailAsync(string emailTo, string emailFrom, string subject, string message)
        {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress(emailTo));
            mail.From = new MailAddress(emailFrom);
            mail.Subject = subject;
            mail.Body = message;

            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient("smtp.sendgrid.net"))
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("azure_2b8d2b151b96baa97e26573e0f44b5e1@azure.com", "Vives2020*");
                    await smtp.SendMailAsync(mail);
                }

            }catch(Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public async Task sendEmailWithAttachementAsync(string emailTo, string emailFrom, string subject, string message, IFormFileCollection files, int? userId = null)
        {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress(emailTo));
            mail.From = new MailAddress(emailFrom);
            mail.Subject = subject;
            mail.Body = message;

            foreach(var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        Attachment att = new Attachment(new MemoryStream(fileBytes), file.FileName);
                        mail.Attachments.Add(att);
                    }
                }
            }

            if(userId != null)
            {
                using(var ms = new MemoryStream())
                {
                    var folderPath = System.IO.Path.Combine("Resources", "users" );
                    var filePath = System.IO.Path.Combine(folderPath, userId.ToString());
                    //var fileFullPath = Path.Combine(filePath, fileName);
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                    var fileName = Directory.GetFiles(fullPath).First();

                    var fullFilePath = System.IO.Path.Combine(fullPath, fileName);


                    var memory = new MemoryStream();
                    using (var stream = new FileStream(fullFilePath, FileMode.Open))
                    {

                        stream.CopyTo(memory);
                        var fileBytes = memory.ToArray();
                        Attachment att = new Attachment(new MemoryStream(fileBytes), fileName);
                        mail.Attachments.Add(att);
                    }
                   
                }
            }
            

            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient("smtp.sendgrid.net"))
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("azure_2b8d2b151b96baa97e26573e0f44b5e1@azure.com", "Vives2020*");
                    await smtp.SendMailAsync(mail);
                }

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

    

    }
}
