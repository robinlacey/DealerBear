using DealerBear.Messages.Interface;

namespace DealerBearTests.Mocks
{
    public class GameSessionNotFoundRequestStub : IGameSessionNotFoundRequest
    {
        public GameSessionNotFoundRequestStub(string sessionID, string messageID)
        {
            SessionID = sessionID;
            MessageID = messageID;
        }

        public string SessionID { get; set; }
        public string MessageID { get; set; }
    }
}