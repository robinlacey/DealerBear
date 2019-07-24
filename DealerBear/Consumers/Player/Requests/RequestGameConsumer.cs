using System.Threading.Tasks;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CheckIfGameInProgress.Interface;
using MassTransit;

namespace DealerBear.Consumers.Player.Requests
{
    public class RequestGameConsumer : IConsumer<IGameRequest>
    {
        private readonly ICheckIfGameInProgress _checkIfGameInProgressUseCase;
        private readonly IAwaitingResponseGateway _responseGateway;

        public RequestGameConsumer(ICheckIfGameInProgress checkIfGameInProgressUseCase,
            IAwaitingResponseGateway responseGateway)
        {
            _checkIfGameInProgressUseCase = checkIfGameInProgressUseCase;
            _responseGateway = responseGateway;
        }

        public async Task Consume(ConsumeContext<IGameRequest> context)
        {
            _checkIfGameInProgressUseCase.Execute(context.Message, _responseGateway, context);
        }
    }
}