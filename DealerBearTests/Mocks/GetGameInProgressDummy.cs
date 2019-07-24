using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GetGameInProgress.Interface;
using MassTransit;

namespace DealerBearTests.Mocks
{
    public class GetGameInProgressDummy : IGetGameInProgress
    {
        public void Execute(string sessionID, IAwaitingResponseGateway awaitingResponseGateway, IPublishEndpoint publishEndPoint)
        {
           
        }
    }
}