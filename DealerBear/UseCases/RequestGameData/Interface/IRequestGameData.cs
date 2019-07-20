using DealerBear.Gateway.Interface;
using MassTransit;
using Messages;

namespace DealerBear.UseCases.RequestGameData.Interface
{
    public interface IRequestGameData
    {
        void Execute(IGameRequest gameRequest, IAwaitingResponseGateway responseGateway, IPublishEndpoint publishEndPoint);
    }
}