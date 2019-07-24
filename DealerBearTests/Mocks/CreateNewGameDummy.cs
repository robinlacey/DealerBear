using DealerBear.Gateway.Interface;
using DealerBear.UseCases.CreateNewGame.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBearTests.Mocks
{
    public class CreateNewGameDummy : ICreateNewGame
    {
        public void Execute(string sessionID, IPackVersionGateway packVersionGateway, IAwaitingResponseGateway awaitingResponseGateway, IGenerateSeed generateSeedUseCase,
            IPublishEndpoint publishEndPoint)
        {
        }
    }
}