using GameBear.Exceptions;
using GameBear.Gateways.Interface;
using GameBear.Messages;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
using MassTransit;
using Messages;

namespace GameBear.UseCases.RequestGameCheckExistingSession
{
    public class RequestGameCheckExistingSession : IRequestGameCheckExistingSession
    {
        public void Execute(IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse, IGameDataGateway gameDataGateway,
            IPublishEndpoint publishEndpoint)
        {
            if (InvalidSessionID(requestGameIsSessionIDInUse))
            {
                throw new InvalidSessionIDException();
            }

            if (gameDataGateway.IsExistingSession(requestGameIsSessionIDInUse.SessionID))
            {
                publishEndpoint.Publish(new RequestGameSessionFound {SessionID = requestGameIsSessionIDInUse.SessionID});
            }
            else
            {
                publishEndpoint.Publish(new RequestGameSessionNotFound {SessionID = requestGameIsSessionIDInUse.SessionID});
            }
        }

        private static bool InvalidSessionID(IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse)
        {
            return requestGameIsSessionIDInUse.SessionID == null ||
                   string.IsNullOrEmpty(requestGameIsSessionIDInUse.SessionID) ||
                   string.IsNullOrWhiteSpace(requestGameIsSessionIDInUse.SessionID);
        }
    }
}