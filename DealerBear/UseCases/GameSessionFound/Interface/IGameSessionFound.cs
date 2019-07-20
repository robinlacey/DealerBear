using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.GetCurrentGameState.Interface;
using MassTransit;

namespace DealerBear.UseCases.GameSessionFound.Interface
{
    public interface IGameSessionFound
    {
        void Execute(IRequestGameSessionFound requestGameSessionFound, IGetCurrentGameState getCurrentGameStateUseCase,
            IAwaitingResponseGateway responseGateway, IPublishEndpoint publishEndPoint);
    }
}