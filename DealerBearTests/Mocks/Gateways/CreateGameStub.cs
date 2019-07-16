using DealerBear.Gateways.Interface;

namespace DealerBearTests.Mocks.Gateways
{
    public class CreateGameStub:ICreateGameGateway
    {
        private readonly string _returnUid;

        public CreateGameStub(string returnUid)
        {
            _returnUid = returnUid;
        }

        public string CreateGame(string gameName, string sessionID) => _returnUid;
    }
}