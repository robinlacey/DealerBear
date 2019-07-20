using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateGameState.Interface;
using MassTransit;

namespace DealerBear.UseCases.GameSessionNotFound.Interface
{
    public interface IGameSessionNotFound
    {
        void Execute(
            IRequestGameSessionNotFound requestGameSessionNotFound, 
            ICreateGameState createGameStateUseCase,
            IAwaitingResponseGateway responseGateway, 
            IPublishEndpoint publishEndPoint);
    }
}