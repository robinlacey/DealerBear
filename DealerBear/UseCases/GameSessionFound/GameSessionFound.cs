using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.GameSessionFound.Interface;
using DealerBear.UseCases.GetCurrentGameState.Interface;
using MassTransit;

namespace DealerBear.UseCases.GameSessionFound
{
    public class GameSessionFound : IGameSessionFound
    {
        public void Execute(
            IRequestGameSessionFound requestGameSessionFound,
            IGetCurrentGameState getCurrentGameStateUseCase,
            IAwaitingResponseGateway responseGateway,
            IPublishEndpoint publishEndPoint)
        {
            if (InvalidMessageID(requestGameSessionFound))
            {
                throw new InvalidMessageIDException();
            }

            if (InvalidSessionID(requestGameSessionFound))
            {
                throw new InvalidSessionIDException();
            }

            if (responseGateway.HasID(requestGameSessionFound.MessageID))
            {
                responseGateway.PopID(requestGameSessionFound.MessageID);
                getCurrentGameStateUseCase.Execute();
            }
        }

        private static bool InvalidMessageID(IRequestGameSessionFound requestGameSessionFound)
        {
            return requestGameSessionFound.MessageID == null ||
                   string.IsNullOrEmpty(requestGameSessionFound.MessageID) ||
                   string.IsNullOrWhiteSpace(requestGameSessionFound.MessageID);
        }

        private static bool InvalidSessionID(IRequestGameSessionFound requestGameSessionFound)
        {
            return requestGameSessionFound.SessionID == null ||
                   string.IsNullOrEmpty(requestGameSessionFound.SessionID) ||
                   string.IsNullOrWhiteSpace(requestGameSessionFound.SessionID);
        }
    }
}