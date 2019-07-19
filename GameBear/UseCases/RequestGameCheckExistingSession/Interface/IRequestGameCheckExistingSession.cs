using GameBear.Gateways.Interface;
using MassTransit;
using Messages;

namespace GameBear.UseCases.RequestGameCheckExistingSession.Interface
{
    public interface IRequestGameCheckExistingSession
    {
        void Execute(IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse, IGameDataGateway gameDataGateway,
            IPublishEndpoint publishEndpoint);
    }
}