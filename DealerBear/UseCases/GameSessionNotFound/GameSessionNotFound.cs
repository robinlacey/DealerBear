using DealerBear.Adaptor.Interface;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateNewGame.Interface;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;

namespace DealerBear.UseCases.GameSessionNotFound
{
    public class GameSessionNotFound : IGameSessionNotFound
    {
        public void Execute(
            IGameSessionNotFoundRequest gameSessionNotFoundRequest,
            ICreateNewGame createNewGame,
            IAwaitingResponseGateway responseGateway,
            IPackVersionGateway packVersionGateway,
            IGenerateSeed generateSeed,
            IPublishMessageAdaptor publishEndPoint)
        {
            if (InvalidMessageID(gameSessionNotFoundRequest))
            {
                throw new InvalidMessageIDException();
            }

            if (InvalidSessionID(gameSessionNotFoundRequest))
            {
                throw new InvalidSessionIDException();
            }

            if (responseGateway.HasID(gameSessionNotFoundRequest.MessageID))
            {
                responseGateway.PopID(gameSessionNotFoundRequest.MessageID);
                createNewGame.Execute(gameSessionNotFoundRequest.SessionID, packVersionGateway,responseGateway, generateSeed,
                    publishEndPoint);
            }
        }

        private static bool InvalidMessageID(IGameSessionNotFoundRequest gameSessionNotFoundRequest)
        {
            return gameSessionNotFoundRequest.MessageID == null ||
                   string.IsNullOrEmpty(gameSessionNotFoundRequest.MessageID) ||
                   string.IsNullOrWhiteSpace(gameSessionNotFoundRequest.MessageID);
        }

        private static bool InvalidSessionID(IGameSessionNotFoundRequest gameSessionNotFoundRequest)
        {
            return gameSessionNotFoundRequest.SessionID == null ||
                   string.IsNullOrEmpty(gameSessionNotFoundRequest.SessionID) ||
                   string.IsNullOrWhiteSpace(gameSessionNotFoundRequest.SessionID);
        }
    }
}