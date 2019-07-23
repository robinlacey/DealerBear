using System.Threading.Tasks;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.RequestGameData.Interface;
using MassTransit;

namespace DealerBear.Consumers
{
    public class RequestGameDataConsumer : IConsumer<IGameRequest>
    {
        private readonly IRequestGameData _requestGameDataUseCase;
        private readonly IAwaitingResponseGateway _responseGateway;

        public RequestGameDataConsumer(IRequestGameData requestGameDataUseCase,
            IAwaitingResponseGateway responseGateway)
        {
            _requestGameDataUseCase = requestGameDataUseCase;
            _responseGateway = responseGateway;
        }

        public async Task Consume(ConsumeContext<IGameRequest> context)
        {
            _requestGameDataUseCase.Execute(context.Message, _responseGateway, context);
        }
    }
}