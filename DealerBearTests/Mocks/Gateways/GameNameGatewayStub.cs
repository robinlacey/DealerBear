using DealerBear_API.Gateways.Interface;

namespace DealerBearTests.Mocks.Gateways
{
    public class GameNameGatewayStub:IGameNamesGateway
    {
        private readonly bool _isValidGameNameReturn;

        public GameNameGatewayStub(bool isValidGameNameReturn)
        {
            _isValidGameNameReturn = isValidGameNameReturn;
        }

        public bool IsValidGameName(string gameName) => _isValidGameNameReturn;
    }
}