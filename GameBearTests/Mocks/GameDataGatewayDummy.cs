using GameBear.Gateways.Interface;
using Messages;

namespace GameBearTests.Mocks
{
    public class GameDataGatewayDummy : IGameDataGateway
    {
        public IGameData GetGameData(string sessionID) => null;

        public bool IsExistingSession(string sessionID) => false;
    }
}