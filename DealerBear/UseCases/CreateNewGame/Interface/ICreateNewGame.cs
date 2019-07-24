using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;

namespace DealerBear.UseCases.CreateNewGame.Interface
{
    public interface ICreateNewGame
    {
        void Execute(string sessionID, string messageID, string startingCardID, int seed, int packVersion, IAwaitingResponseGateway awaitingResponseGateway, IPublishMessageAdaptor publishMessageAdaptor);
    }
}