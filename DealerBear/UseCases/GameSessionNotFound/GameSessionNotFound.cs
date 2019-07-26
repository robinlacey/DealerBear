using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using IGetStartingCard = DealerBear.UseCases.GetStartingCard.Interface.IGetStartingCard;

namespace DealerBear.UseCases.GameSessionNotFound
{
    public class GameSessionNotFound : IGameSessionNotFound
    {
        private readonly IGetStartingCard _getStartingCard;
        private readonly IAwaitingResponseGateway _responseGateway;

        public GameSessionNotFound(
            IGetStartingCard getStartingCard,
            IAwaitingResponseGateway responseGateway)
        {
            _getStartingCard = getStartingCard;
            _responseGateway = responseGateway;
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

            if (_responseGateway.HasID(messageID))
            {
                _responseGateway.PopID(messageID);
                _getStartingCard.Execute(sessionID);
            }
        }

        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}