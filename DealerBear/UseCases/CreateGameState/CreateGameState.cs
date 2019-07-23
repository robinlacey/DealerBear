using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBear.UseCases.CreateGameState
{
    public class CreateGameState : ICreateGameState
    {
        public void Execute(string sessionID, IPackVersionGateway packVersionGateway, IGenerateSeed generateSeedUseCase,
            IPublishEndpoint publishEndPoint)
        {
            if (InvalidSessionID(sessionID))
            {
                throw new InvalidSessionIDException();
            }

            publishEndPoint.Publish(new CreateNewGameData
            {
                SessionID = sessionID,
                PackVersionNumber = packVersionGateway.GetCurrentPackVersion(),
                Seed = generateSeedUseCase.Execute()
            });
        }


        private static bool InvalidSessionID(string sessionID)
        {
            return sessionID == null || string.IsNullOrEmpty(sessionID) || string.IsNullOrWhiteSpace(sessionID);
        }
    }
}