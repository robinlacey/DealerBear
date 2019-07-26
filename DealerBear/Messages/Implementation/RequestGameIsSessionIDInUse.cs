using DealerBear.Messages.Interface;

namespace DealerBear.Messages.Implementation
{
    public class RequestGameIsSessionIDInUse : IRequestGameIsSessionIDInUse
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }
    }
}