using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CheckIfGameInProgress.Interface;

namespace DealerBear.UseCases.CheckIfGameInProgress
{
    public class CheckIfGameInProgress : ICheckIfGameInProgress
    {
        public void Execute(IGameRequest gameRequest, IAwaitingResponseGateway responseGateway,
            IPublishMessageAdaptor publishEndPoint)
        {
            if (InvalidSessionID(gameRequest))
            {
                throw new InvalidSessionIDException();
            }

            string messageID = Guid.NewGuid().ToString();
            responseGateway.SaveID(messageID);
            publishEndPoint.Publish(new RequestGameIsSessionIDInUse
            {
                SessionID = gameRequest.SessionID,
                MessageID = messageID
            });
        }

        private static bool InvalidSessionID(IGameRequest gameRequest)
        {
            return gameRequest.SessionID == null ||
                   string.IsNullOrEmpty(gameRequest.SessionID) ||
                   string.IsNullOrWhiteSpace(gameRequest.SessionID);
        }
    }
}