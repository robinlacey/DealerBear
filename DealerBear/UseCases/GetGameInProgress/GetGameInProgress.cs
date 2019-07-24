using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.Messages.Implementation;
using DealerBear.UseCases.GetGameInProgress.Interface;

namespace DealerBear.UseCases.GetGameInProgress
{
    public class GetGameInProgress : IGetGameInProgress
    {
        public void Execute(string sessionID, IAwaitingResponseGateway awaitingResponseGateway, IPublishMessageAdaptor publishEndPoint)
        {
            if (InvalidSessionID(sessionID))
            {
                throw new InvalidSessionIDException();
            }
            string messageID = Guid.NewGuid().ToString();
            publishEndPoint.Publish(new GetCurrentGameData
            {
                SessionID = sessionID,
                MessageID = messageID
            });
            awaitingResponseGateway.SaveID(messageID);
        }
        
        private static bool InvalidSessionID(string sessionID)
        {
            return sessionID == null ||
                   string.IsNullOrEmpty(sessionID) ||
                   string.IsNullOrWhiteSpace(sessionID);
        }
    }
}