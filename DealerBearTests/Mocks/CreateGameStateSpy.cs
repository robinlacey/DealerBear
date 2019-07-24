using DealerBear.Gateway.Interface;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBearTests.Mocks
{
    public class CreateGameStateSpy : ICreateGameState
    {
        public bool ExecuteCalled { get; private set; }

        public void Execute(string sessionID, IPackVersionGateway packVersionGateway,IAwaitingResponseGateway awaitingResponseGateway,  IGenerateSeed generateSeedUseCase,
            IPublishEndpoint publishEndPoint)
        {
            ExecuteCalled = true;
        }
    }
}