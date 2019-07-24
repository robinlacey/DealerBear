using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;

namespace DealerBear.UseCases.GetStartingCard.Interface
{
    public interface IGetStartingCard
    {
        void Execute(
            string sessionID,
            IPackVersionGateway packVersionGateway,
            IAwaitingResponseGateway awaitingResponseGateway,
            IGenerateSeed generateSeedUseCase,
            IPublishMessageAdaptor publishEndPoint);
    }
}