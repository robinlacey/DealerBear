using DealerBear.Gateway.Interface;
using MassTransit;

namespace DealerBear.UseCases.GetGameInProgress.Interface
{
    public interface IGetGameInProgress
    {
        void Execute(
            string sessionID,
            IAwaitingResponseGateway awaitingResponseGateway,
            IPublishEndpoint publishEndPoint);
    }
}