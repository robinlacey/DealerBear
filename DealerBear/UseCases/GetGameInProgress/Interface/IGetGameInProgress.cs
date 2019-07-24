using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;

namespace DealerBear.UseCases.GetGameInProgress.Interface
{
    public interface IGetGameInProgress
    {
        void Execute(
            string sessionID,
            IAwaitingResponseGateway awaitingResponseGateway,
            IPublishMessageAdaptor publishEndPoint);
    }
}