using DealerBear_API.Gateways.Interface;

namespace DealerBearTests.Mocks.Gateways
{
    public class GameNamesGatewayDummy:IGameNamesGateway
    {
        public bool IsValidGameName(string gameName) => true;
    }
}