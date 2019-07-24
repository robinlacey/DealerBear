using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;
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
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;
        public RequestGameConsumer(
            ICheckIfGameInProgress checkIfGameInProgressUseCase,
            IAwaitingResponseGateway responseGateway,
            IPublishMessageAdaptor publishMessageAdaptor)
        {

            _publishMessageAdaptor = publishMessageAdaptor;
            _checkIfGameInProgressUseCase = checkIfGameInProgressUseCase;
            _responseGateway = responseGateway;
        }

        public async Task Consume(ConsumeContext<IGameRequest> context)
        {
            _checkIfGameInProgressUseCase.Execute(context.Message, _responseGateway, _publishMessageAdaptor);
        }
    }
}