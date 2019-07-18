using GameBear.Exceptions;
using GameBear.Gateways.Interface;
using GameBear.UseCases.IsExistingSession.Interface;
using MassTransit;
using Messages;

namespace GameBear.UseCases.IsExistingSession
{
    public class IsExistingSession:IIsExistingSession
    {
        public void Execute(IIsSessionIDInUse isSessionIDInUse, IGameDataGateway gameDataGateway, IPublishEndpoint publishEndpoint)
        {
            if (InvalidSessionID(isSessionIDInUse))
            {
                throw new InvalidSessionIDException();
            }
        }
        
        private static bool InvalidSessionID(IIsSessionIDInUse isSessionIDInUse)
        {
            return isSessionIDInUse.SessionID == null ||
                   string.IsNullOrEmpty(isSessionIDInUse.SessionID) ||
                   string.IsNullOrWhiteSpace(isSessionIDInUse.SessionID);
        }
    }
}