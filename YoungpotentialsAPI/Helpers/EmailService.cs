using System;
using System.Collections.Generic;
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
                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("george.desmet1998@gmail.com", "youngP0tentials*5");
                    await smtp.SendMailAsync(mail);
                }

            }catch(Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

    }
}
