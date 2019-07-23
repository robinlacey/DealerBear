using DealerBear.Messages;

namespace DealerBearTests.Mocks
{
    public class RequestGameSessionNotFoundStub : IRequestGameSessionNotFound
    {
        public RequestGameSessionNotFoundStub(string sessionID, string messageID)
        {
            SessionID = sessionID;
            MessageID = messageID;
        }

        public string SessionID { get; set; }
        public string MessageID { get; set; }
    }
}