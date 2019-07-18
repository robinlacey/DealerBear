using System;
using DealerBear.Consumers;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DealerBear
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello DealerBear!");
            string rabbitMQHost = $"rabbitmq://{Environment.GetEnvironmentVariable("RABBITMQ_HOST")}";
            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                IRabbitMqHost host = sbc.Host(new Uri(rabbitMQHost), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                sbc.ReceiveEndpoint(host, "RequestGameData",
                    ep => { ep.Consumer(() => new RequestGameDataConsumer()); });
            });
            bus.Start();
        }
    }
}