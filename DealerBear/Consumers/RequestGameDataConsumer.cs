using System;
using System.Threading.Tasks;
using DealerBear_API.Exceptions;
using DealerBear_API.Messages;
using DealerBear_API.UseCases.RequestGameData;
using DealerBear_API.UseCases.StartGame.Interface;
using MassTransit;
using Messages;

namespace DealerBear_API.Consumers
{
    public class RequestGameDataConsumer : IConsumer<IGameRequest>
    {
        public async Task Consume(ConsumeContext<IGameRequest> context)
        {
            IRequestGameData requestGameData = new RequestGameData();
            requestGameData.Execute(context.Message, context);
        }
    }
}