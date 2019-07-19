using GameBear.Gateways.Interface;
using Messages;

namespace GameBearTests.Mocks
{
    public class GameDataGatewayStub : IGameDataGateway
    {
        private readonly IGameData _getGameDataReturn;
        private readonly bool _isExistingSessionReturn;

        public GameDataGatewayStub(IGameData getGameDataReturn, bool isExistingSessionReturn)
        {
            _getGameDataReturn = getGameDataReturn;
            _isExistingSessionReturn = isExistingSessionReturn;
        }

        public IGameData GetGameData(string sessionID) => _getGameDataReturn;

        public bool IsExistingSession(string sessionID) => _isExistingSessionReturn;
    }
}