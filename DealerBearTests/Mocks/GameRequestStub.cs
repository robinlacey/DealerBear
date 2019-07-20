using Messages;

namespace DealerBearTests.Mocks
{
    public class GameRequestStub : IGameRequest
    {
        public string SessionID { get; set; }

        public GameRequestStub(string sessionID)
        {
            SessionID = sessionID;
        }
    }
}