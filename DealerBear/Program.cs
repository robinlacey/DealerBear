using System;
using DealerBear_API.Consumers;
using Messages;
using MassTransit;

namespace DealerBear_API
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello DealerBear!" + new SimpleMessage() {Text = " Message Library Built"}.Text);
            string rabbitMQHost = $"rabbitmq://{Environment.GetEnvironmentVariable("RABBITMQ_HOST")}";
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(rabbitMQHost), h =>
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