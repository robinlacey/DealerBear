using Messages;

namespace DealerBear_API.Messages
{
    public class IsSessionIDInUse : IIsSessionIDInUse
    {
        public string SessionID { get; set; }
    }
}