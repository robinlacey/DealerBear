using GameBear.Gateways.Interface;
using Messages;

namespace GameBear.Gateways
{
    public class InMemoryGameDataGateway : IGameDataGateway
    {
        public IGameData GetGameData(string sessionID)
        {
            return null;
            throw new System.NotImplementedException();
        }

        public bool IsExistingSession(string sessionID)
        {
            return false;
            throw new System.NotImplementedException();
        }
    }
}