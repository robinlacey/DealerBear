using MassTransit;
using Messages;

namespace DealerBear_API.UseCases.StartGame.Interface
{
    public interface IRequestGameData
    {
        void Execute(IGameRequest gameRequest, IPublishEndpoint publishEndPoint);
    }
}