using System;
using System.Threading.Tasks;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.RequestGameData;
using DealerBear.UseCases.RequestGameData.Interface;
using MassTransit;
using Messages;

namespace DealerBear.Consumers
{
    public class RequestGameDataConsumer : IConsumer<IGameRequest>
    {
        private readonly IRequestGameData _requestGameDataUseCase;
        private readonly IAwaitingResponseGateway _responseGateway;

        public RequestGameDataConsumer(IRequestGameData requestGameDataUseCase, IAwaitingResponseGateway responseGateway)
        {
            _requestGameDataUseCase = requestGameDataUseCase;
            _responseGateway = responseGateway;
        }
        public async Task Consume(ConsumeContext<IGameRequest> context)
        {
            Console.WriteLine("Recieved IGameRequest");
            _requestGameDataUseCase.Execute(context.Message, _responseGateway, context);
        }
    }
}