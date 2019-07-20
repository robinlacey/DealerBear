using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using MassTransit;

namespace DealerBear.UseCases.RequestGameData.Interface
{
    public interface IRequestGameData
    {
        void Execute(IGameRequest gameRequest, IAwaitingResponseGateway responseGateway,
            IPublishEndpoint publishEndPoint);
    }
}