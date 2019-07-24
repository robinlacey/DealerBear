using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;

namespace DealerBear.UseCases.CheckIfGameInProgress.Interface
{
    public interface ICheckIfGameInProgress
    {
        void Execute(IGameRequest gameRequest, IAwaitingResponseGateway responseGateway,
            IPublishMessageAdaptor publishEndPoint);
    }
}