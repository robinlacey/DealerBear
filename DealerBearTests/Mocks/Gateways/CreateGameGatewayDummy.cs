using DealerBear.Gateways.Interface;

namespace DealerBearTests.Mocks.Gateways
{
    public class CreateGameGatewayDummy:ICreateGameGateway
    {
        public string CreateGame(string gameName, string sessionID) => string.Empty;
    }
}