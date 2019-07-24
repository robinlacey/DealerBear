using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GetGameInProgress.Interface;

namespace DealerBearTests.Mocks
{
    public class GetGameInProgressDummy : IGetGameInProgress
    {
        public void Execute(string sessionID, IAwaitingResponseGateway awaitingResponseGateway, IPublishMessageAdaptor publishEndPoint)
        {
           
        }
    }
}