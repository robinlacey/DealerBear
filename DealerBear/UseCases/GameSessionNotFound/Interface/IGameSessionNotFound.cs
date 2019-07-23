using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBear.UseCases.GameSessionNotFound.Interface
{
    public interface IGameSessionNotFound
    {
        void Execute(
            IRequestGameSessionNotFound requestGameSessionNotFound,
            ICreateGameState createGameState,
            IAwaitingResponseGateway responseGateway,
            IPackVersionGateway packVersionGateway,
            IGenerateSeed generateSeed,
            IPublishEndpoint publishEndPoint);
    }
}