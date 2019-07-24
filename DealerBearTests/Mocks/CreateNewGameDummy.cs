using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.CreateNewGame.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;

namespace DealerBearTests.Mocks
{
    public class CreateNewGameDummy : ICreateNewGame
    {
        public void Execute(string sessionID, IPackVersionGateway packVersionGateway, IAwaitingResponseGateway awaitingResponseGateway, IGenerateSeed generateSeedUseCase,
            IPublishMessageAdaptor publishEndPoint)
        {
        }
    }
}