using GameBear.Gateways.Interface;
using MassTransit;
using Messages;

namespace GameBear.UseCases.IsExistingSession.Interface
{
    public interface IIsExistingSession
    {
        void Execute(IIsSessionIDInUse isSessionIDInUse, IGameDataGateway gameDataGateway, IPublishEndpoint publishEndpoint);
    }
}