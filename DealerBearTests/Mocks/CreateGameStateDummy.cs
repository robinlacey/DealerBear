using DealerBear.Gateway.Interface;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBearTests.Mocks
{
    public class CreateGameStateDummy : ICreateGameState
    {
        public void Execute(string sessionID, IPackVersionGateway packVersionGateway, IGenerateSeed generateSeedUseCase,
            IPublishEndpoint publishEndPoint)
        {
        }
    }
}