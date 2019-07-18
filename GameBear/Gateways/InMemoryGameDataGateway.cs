using GameBear.Gateways.Interface;
using Messages;

namespace GameBear.Gateways
{
    public class InMemoryGameDataGateway:IGameDataGateway
    {
        public IGameData GetGameData(string sessionID)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistingSession(string sessionID)
        {
            throw new System.NotImplementedException();
        }
    }
}