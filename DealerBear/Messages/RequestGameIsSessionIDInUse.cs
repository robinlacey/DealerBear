using Messages;

namespace DealerBear.Messages
{
    public class RequestGameIsSessionIDInUse : IRequestGameIsSessionIDInUse
    {
        public string SessionID { get; set; }
    }
}