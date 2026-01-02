using BusinessLogicLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using ModelLayer.DTO;

public class RabbitMQProducer : IRabbitMQProducer, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;

    public RabbitMQProducer(IConfiguration configuration)
    {
        _configuration = configuration;

        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:HostName"],
            UserName = _configuration["RabbitMQ:UserName"],
            Password = _configuration["RabbitMQ:Password"]
        };

        _connection = factory.CreateConnection(); 
    }

    public void SendEmailMessage(EmailRequest emailMessage)
    {
        using var channel = _connection.CreateModel(); 

        channel.QueueDeclare(
            queue: _configuration["RabbitMQ:QueueName"],
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(emailMessage));

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(
            exchange: "",
            routingKey: _configuration["RabbitMQ:QueueName"],
            basicProperties: properties,
            body: body);
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}
