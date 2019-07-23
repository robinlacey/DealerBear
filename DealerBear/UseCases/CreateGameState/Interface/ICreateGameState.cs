using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBear.UseCases.CreateGameState.Interface
{
    public interface ICreateGameState
    {
        void Execute(
            string sessionID,
            IPackVersionGateway packVersionGateway,
            IGenerateSeed generateSeedUseCase,
            IPublishEndpoint publishEndPoint);
    }
}