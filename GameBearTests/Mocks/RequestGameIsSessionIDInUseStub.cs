using Messages;

namespace GameBearTests.Mocks
{
    public class RequestGameIsSessionIDInUseStub : IRequestGameIsSessionIDInUse
    {
        public RequestGameIsSessionIDInUseStub(string sessionID)
        {
            SessionID = sessionID;
        }

        public string SessionID { get; set; }
    }
}