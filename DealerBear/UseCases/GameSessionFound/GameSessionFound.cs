using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GameSessionFound.Interface;
using DealerBear.UseCases.GetGameInProgress.Interface;

namespace DealerBear.UseCases.GameSessionFound
{
    public class GameSessionFound : IGameSessionFound
    {
        private readonly IGetGameInProgress _getGameInProgressUseCase;
        private readonly IAwaitingResponseGateway _awaitingResponseGateway;

        public GameSessionFound(IGetGameInProgress getGameInProgressUseCase,
            IAwaitingResponseGateway awaitingResponseGateway)
        {
            _getGameInProgressUseCase = getGameInProgressUseCase;
            _awaitingResponseGateway = awaitingResponseGateway;
        }
        public void Execute(string sessionID, string messageID)
            {
            if (InvalidIDString(messageID))
            {
                throw new InvalidMessageIDException();
            }

            if (InvalidIDString(sessionID))
            {
                throw new InvalidSessionIDException();
            }

            if (!_awaitingResponseGateway.HasID(messageID))
            {
                return;
            }
            
            _awaitingResponseGateway.PopID(messageID);
            _getGameInProgressUseCase.Execute(sessionID);
        }

        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}