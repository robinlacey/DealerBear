using DealerBear.Messages;

namespace DealerBearTests.Mocks
{
    public class RequestGameSessionFoundStub : IRequestGameSessionFound
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }

        public RequestGameSessionFoundStub(string sessionId, string messageID)
        {
            SessionID = sessionId;
            MessageID = messageID;
        }
    }
}