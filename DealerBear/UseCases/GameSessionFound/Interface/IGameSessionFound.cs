using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.GetGameInProgress.Interface;


namespace DealerBear.UseCases.GameSessionFound.Interface
{
    public interface IGameSessionFound
    {
        void Execute(IRequestGameSessionFound requestGameSessionFound, IGetGameInProgress getGameInProgressUseCase,
            IAwaitingResponseGateway awaitingResponseGateway, IPublishMessageAdaptor publishEndPoint);
    }
}