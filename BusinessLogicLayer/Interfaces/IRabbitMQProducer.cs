using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO;
using ModelLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IRabbitMQProducer
    {
        void SendEmailMessage(EmailRequest emailMessage);
    }

}
