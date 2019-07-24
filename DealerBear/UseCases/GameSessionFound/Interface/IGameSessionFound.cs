using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.GetGameInProgress.Interface;
using MassTransit;

namespace DealerBear.UseCases.GameSessionFound.Interface
{
    public interface IGameSessionFound
    {
        void Execute(IRequestGameSessionFound requestGameSessionFound, IGetGameInProgress getGameInProgressUseCase,
            IAwaitingResponseGateway awaitingResponseGateway, IPublishEndpoint publishEndPoint);
    }
}