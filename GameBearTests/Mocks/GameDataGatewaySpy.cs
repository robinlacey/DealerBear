using GameBear.Gateways.Interface;
using Messages;

namespace GameBearTests.Mocks
{
    public class GameDataGatewaySpy : IGameDataGateway
    {
        public string GetGameDataSessionID { get; private set; }

        public IGameData GetGameData(string sessionID)
        {
            GetGameDataSessionID = sessionID;
            return null;
        }

        public string IsExistingSessionSessionID { get; private set; }

        public bool IsExistingSession(string sessionID)
        {
            IsExistingSessionSessionID = sessionID;
            return false;
        }
    }
}