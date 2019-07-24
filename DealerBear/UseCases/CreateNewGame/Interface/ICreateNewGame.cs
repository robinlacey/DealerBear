using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;

namespace DealerBear.UseCases.CreateNewGame.Interface
{
    public interface ICreateNewGame
    {
        void Execute(
            string sessionID,
            IPackVersionGateway packVersionGateway,
            IAwaitingResponseGateway awaitingResponseGateway,
            IGenerateSeed generateSeedUseCase,
            IPublishMessageAdaptor publishEndPoint);
    }
}