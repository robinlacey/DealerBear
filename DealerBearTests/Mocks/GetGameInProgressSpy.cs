using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GetGameInProgress.Interface;
using MassTransit;

namespace DealerBearTests.Mocks
{
    public class GetGameInProgressSpy : IGetGameInProgress
    {
        public bool ExecuteCalled { get; private set; }

        public void Execute(string sessionID, IAwaitingResponseGateway awaitingResponseGateway, IPublishEndpoint publishEndPoint)
        {
            ExecuteCalled = true;
        }
    }
}