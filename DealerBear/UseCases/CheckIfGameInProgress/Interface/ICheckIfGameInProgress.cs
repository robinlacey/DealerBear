using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using MassTransit;

namespace DealerBear.UseCases.CheckIfGameInProgress.Interface
{
    public interface ICheckIfGameInProgress
    {
        void Execute(IGameRequest gameRequest, IAwaitingResponseGateway responseGateway,
            IPublishEndpoint publishEndPoint);
    }
}