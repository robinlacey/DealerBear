using DealerBear.UseCases.GameSessionNotFound.Interface;
using MassTransit;
using Messages;

namespace DealerBear.UseCases.GameSessionNotFound
{
    public class GameSessionNotFound:IGameSessionNotFound
    {
        public void Execute(IRequestGameSessionNotFound requestGameSessionNotFound, IPublishEndpoint publishEndPoint)
        {
            // TIME TO SORT OUT IDOOTENCY !!
            // Only want to deal with this request once
            // need a transaction id and a gateway to check 
            throw new System.NotImplementedException();
        }
    }
}