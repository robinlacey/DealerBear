using DealerBear_API.Exceptions;
using DealerBear_API.Messages;
using DealerBear_API.UseCases.StartGame.Interface;
using MassTransit;
using Messages;

namespace DealerBear_API.UseCases.RequestGameData
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