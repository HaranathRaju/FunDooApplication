using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class EmailRequest
    {
        public EmailRequest(string toEmail, string subject, string body)
        {
            ToEmail = toEmail;
            Subject = subject;
            Body = body;
        }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }


    }
}
