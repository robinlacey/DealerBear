using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateNewGame.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;

namespace DealerBear.UseCases.CreateNewGame
{
    public class CreateNewGame : ICreateNewGame
    {
        public void Execute(
            string sessionID, 
            IPackVersionGateway packVersionGateway, 
            IAwaitingResponseGateway awaitingResponseGateway,
            IGenerateSeed generateSeedUseCase,
            IPublishMessageAdaptor publishEndPoint)
        {
            if (InvalidSessionID(sessionID))
            {
                throw new InvalidSessionIDException();
            }

            string messageID = Guid.NewGuid().ToString();
            publishEndPoint.Publish(new CreateNewGameData
            {
                MessageID = messageID,
                SessionID = sessionID,
                PackVersionNumber = packVersionGateway.GetCurrentPackVersion(),
                Seed = generateSeedUseCase.Execute(),
            });
            awaitingResponseGateway.SaveID(messageID);
        }


        private static bool InvalidSessionID(string sessionID)
        {
            return sessionID == null || string.IsNullOrEmpty(sessionID) || string.IsNullOrWhiteSpace(sessionID);
        }
    }
}