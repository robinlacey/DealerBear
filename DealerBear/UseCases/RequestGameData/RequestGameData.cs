using DealerBear.Exceptions;
using DealerBear.Messages;
using DealerBear.UseCases.RequestGameData.Interface;
using MassTransit;
using Messages;

namespace DealerBear.UseCases.RequestGameData
{
    public class RequestGameData : IRequestGameData
    {
        public void Execute(IGameRequest gameRequest, IPublishEndpoint publishEndPoint)
        {
            if (InvalidSessionID(gameRequest))
            {
                throw new InvalidSessionIDException();
            }

            publishEndPoint.Publish(new IsSessionIDInUse {SessionID = gameRequest.SessionID});
        }

        private static bool InvalidSessionID(IGameRequest gameRequest)
        {
            return gameRequest.SessionID == null ||
                   string.IsNullOrEmpty(gameRequest.SessionID) ||
                   string.IsNullOrWhiteSpace(gameRequest.SessionID);
        }
    }
}