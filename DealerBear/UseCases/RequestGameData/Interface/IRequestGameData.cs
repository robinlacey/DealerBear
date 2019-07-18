using MassTransit;
using Messages;

namespace DealerBear.UseCases.RequestGameData.Interface
{
    public interface IRequestGameData
    {
        void Execute(IGameRequest gameRequest, IPublishEndpoint publishEndPoint);
    }
}