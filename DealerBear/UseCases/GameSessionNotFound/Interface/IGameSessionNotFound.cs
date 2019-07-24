using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateNewGame.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBear.UseCases.GameSessionNotFound.Interface
{
    public interface IGameSessionNotFound
    {
        void Execute(
            IGameSessionNotFoundRequest gameSessionNotFoundRequest,
            ICreateNewGame createNewGame,
            IAwaitingResponseGateway responseGateway,
            IPackVersionGateway packVersionGateway,
            IGenerateSeed generateSeed,
            IPublishEndpoint publishEndPoint);
    }
}