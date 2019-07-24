using System;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.GetGameInProgress.Interface;
using MassTransit;

namespace DealerBear.UseCases.GetGameInProgress
{
    public class GetGameInProgress : IGetGameInProgress
    {
        public void Execute(string sessionID, IAwaitingResponseGateway awaitingResponseGateway, IPublishEndpoint publishEndPoint)
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