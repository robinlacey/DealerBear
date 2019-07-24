using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GetGameInProgress.Interface;

namespace DealerBearTests.Mocks
{
    public class GetGameInProgressSpy : IGetGameInProgress
    {
        public bool ExecuteCalled { get; private set; }

        public void Execute(string sessionID, IAwaitingResponseGateway awaitingResponseGateway, IPublishMessageAdaptor publishEndPoint)
        {
            ExecuteCalled = true;
        }
    }
}