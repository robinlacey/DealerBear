using System;
using IntegrationTests.Consumers;
using IntegrationTests.Messages;
using MassTransit;
using Messages;

namespace IntegrationTests
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            IBusControl bus = SetupRabbitMQ();
            bus.Start();
            
            // Integration Tests
            // Publish Start Game
            StartNewGame(bus, "Scout The Dog");
            int secondsWaited = 0;
            while (secondsWaited < 10)
            {
                System.Threading.Thread.Sleep(1000);
                secondsWaited++;
                if (AcceptanceTestStages.ReturnedCorrectStartGameCardID)
                {
                    Console.WriteLine("Correctly returned StartGame ..");
                    break;
                }
            }
            if (secondsWaited == 10)
            {
                Console.WriteLine("Failed StartGame...");
                return (int) ExitCode.TimeOutFailure;
            }

            secondsWaited = 0;
            return (int) ExitCode.Success;
        }

        private static IBusControl SetupRabbitMQ()
        {
            string rabbitMQHost = $"rabbitmq://{Environment.GetEnvironmentVariable("RABBITMQ_HOST")}";
            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(rabbitMQHost), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "ReceivedGameData",
                    ep => { ep.Consumer(() => new ReceivedGameDataConsumer()); });
            });
            return bus;
        }

        private static void StartNewGame(IBusControl bus, string sessionID)
        {
            bus.Publish(new StartGameRequestMessage {SessionID = sessionID});
        }
    }
}

public class AcceptanceTestStages
{
    public static bool ReturnedCorrectStartGameCardID = false;
}