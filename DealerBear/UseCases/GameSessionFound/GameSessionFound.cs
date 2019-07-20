using DealerBear.UseCases.RequestGameSessionFound.Interface;
using MassTransit;
using Messages;

namespace DealerBear.UseCases.RequestGameSessionFound
{
    public class GameSessionFound:IGameSessionFound
    {
        public void Execute(IRequestGameSessionFound requestGameSessionFound, IPublishEndpoint publishEndPoint)
        {
            
        }
    }
}