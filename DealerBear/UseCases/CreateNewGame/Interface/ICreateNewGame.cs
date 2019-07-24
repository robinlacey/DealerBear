using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBear.UseCases.CreateNewGame.Interface
{
    public interface ICreateNewGame
    {
        void Execute(
            string sessionID,
            IPackVersionGateway packVersionGateway,
            IAwaitingResponseGateway awaitingResponseGateway,
            IGenerateSeed generateSeedUseCase,
            IPublishEndpoint publishEndPoint);
    }
}