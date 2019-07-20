using MassTransit;
using Messages;

namespace DealerBear.UseCases.GameSessionNotFound.Interface
{
    public interface IGameSessionNotFound
    {
        void Execute(IRequestGameSessionNotFound requestGameSessionNotFound, IPublishEndpoint publishEndPoint);
    }
}