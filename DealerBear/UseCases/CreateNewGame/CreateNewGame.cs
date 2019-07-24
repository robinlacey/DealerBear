using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages.Implementation;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.CreateNewGame.Interface;

namespace DealerBear.UseCases.CreateNewGame
{
    public class CreateNewGame:ICreateNewGame
    {
        public void Execute(string sessionID, string messageID, string startingCardID, int seed, int packVersion,
            IAwaitingResponseGateway awaitingResponseGateway, IPublishMessageAdaptor publishMessageAdaptor)
        {
            if (InvalidMessageID(messageID))
            {
                throw new InvalidMessageIDException();
            }

            if (InvalidSessionID(sessionID))
            {
                throw new InvalidSessionIDException();
            }

            if (awaitingResponseGateway.HasID(messageID))
            {
                awaitingResponseGateway.PopID(messageID);
                string newMessageID = Guid.NewGuid().ToString();
                publishMessageAdaptor.Publish(new CreateNewGameRequest
                {
                    SessionID = sessionID,
                    MessageID = newMessageID,
                    StartingCardID = startingCardID,
                    Seed = seed,
                    PackVersionNumber = packVersion
                });
                awaitingResponseGateway.SaveID(newMessageID);
                
            }
        }
        private static bool InvalidMessageID(string messageID)
        {
            return messageID == null ||
                   string.IsNullOrEmpty(messageID) ||
                   string.IsNullOrWhiteSpace(messageID);
        }

        private static bool InvalidSessionID(string sessionID)
        {
            return sessionID == null ||
                   string.IsNullOrEmpty(sessionID) ||
                   string.IsNullOrWhiteSpace(sessionID);
        }
    }
    
    
}