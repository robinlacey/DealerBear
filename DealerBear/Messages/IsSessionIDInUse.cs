using Messages;

namespace DealerBear.Messages
{
    public class IsSessionIDInUse : IIsSessionIDInUse
    {
        public string SessionID { get; set; }
    }
}