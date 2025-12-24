using BusinessLogicLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(EmailRequest request)
        {
           
            var smtp = configuration.GetSection("SmtpSettings");

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(
                    smtp["SenderEmail"],
                    smtp["SenderName"]
                ),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = true
            };

            mail.To.Add(request.ToEmail);

        
            SmtpClient client = new SmtpClient(smtp["Server"])
            {
                Port = int.Parse(smtp["Port"]),
                Credentials = new NetworkCredential(
                    smtp["Username"],
                    smtp["Password"]
                ),
                EnableSsl = bool.Parse(smtp["EnableSsl"])
            };

            client.Send(mail);
        }
    }
}
