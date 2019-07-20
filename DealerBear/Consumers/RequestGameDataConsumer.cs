using System;
using System.Threading.Tasks;
using DealerBear.UseCases.RequestGameData;
using DealerBear.UseCases.RequestGameData.Interface;
using MassTransit;
using Messages;

namespace DealerBear.Consumers
{
    public class RequestGameDataConsumer : IConsumer<IGameRequest>
    {
        public async Task Consume(ConsumeContext<IGameRequest> context)
        {
            Console.WriteLine("Recieved IGameRequest");
            IRequestGameData requestGameData = new RequestGameData();
            requestGameData.Execute(context.Message, context);
        }
    }
}