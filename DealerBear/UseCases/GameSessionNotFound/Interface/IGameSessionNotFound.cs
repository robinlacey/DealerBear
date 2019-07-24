using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using IGetStartingCard = DealerBear.UseCases.GetStartingCard.Interface.IGetStartingCard;

namespace DealerBear.UseCases.GameSessionNotFound.Interface
{
    public interface IGameSessionNotFound
    {
        void Execute(
            IGameSessionNotFoundRequest gameSessionNotFoundRequest,
            IGetStartingCard getStartingCard,
            IAwaitingResponseGateway responseGateway,
            IPackVersionGateway packVersionGateway,
            IGenerateSeed generateSeed,
            IPublishMessageAdaptor publishEndPoint);
    }
}