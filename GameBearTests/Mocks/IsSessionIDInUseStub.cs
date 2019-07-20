using Messages;

namespace GameBearTests.Mocks
{
    public class IsSessionIDInUseStub:IIsSessionIDInUse
    {

        public IsSessionIDInUseStub(string sessionID)
        {
            SessionID = sessionID;
        }
        public string SessionID { get; set; }
    }
}