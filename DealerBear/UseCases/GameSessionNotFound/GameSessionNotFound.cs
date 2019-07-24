using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;

namespace DealerBear.UseCases.GameSessionNotFound
{
    public class GameSessionNotFound : IGameSessionNotFound
    {
        public void Execute(
            IRequestGameSessionNotFound requestGameSessionNotFound,
            ICreateGameState createGameState,
            IAwaitingResponseGateway responseGateway,
            IPackVersionGateway packVersionGateway,
            IGenerateSeed generateSeed,
            IPublishEndpoint publishEndPoint)
        {
            if (InvalidMessageID(requestGameSessionNotFound))
            {
                throw new InvalidMessageIDException();
            }

            if (InvalidSessionID(requestGameSessionNotFound))
            {
                throw new InvalidSessionIDException();
            }

            if (responseGateway.HasID(requestGameSessionNotFound.MessageID))
            {
                responseGateway.PopID(requestGameSessionNotFound.MessageID);
                createGameState.Execute(requestGameSessionNotFound.SessionID, packVersionGateway,responseGateway, generateSeed,
                    publishEndPoint);
            }
        }

        private static bool InvalidMessageID(IRequestGameSessionNotFound requestGameSessionNotFound)
        {
            return requestGameSessionNotFound.MessageID == null ||
                   string.IsNullOrEmpty(requestGameSessionNotFound.MessageID) ||
                   string.IsNullOrWhiteSpace(requestGameSessionNotFound.MessageID);
        }

        private static bool InvalidSessionID(IRequestGameSessionNotFound requestGameSessionNotFound)
        {
            return requestGameSessionNotFound.SessionID == null ||
                   string.IsNullOrEmpty(requestGameSessionNotFound.SessionID) ||
                   string.IsNullOrWhiteSpace(requestGameSessionNotFound.SessionID);
        }
    }
}