using MassTransit;
using Messages;

namespace DealerBear.UseCases.RequestGameSessionFound.Interface
{
    public interface IGameSessionFound
    {
        void Execute(IRequestGameSessionFound requestGameSessionFound, IPublishEndpoint publishEndPoint);
    }
}