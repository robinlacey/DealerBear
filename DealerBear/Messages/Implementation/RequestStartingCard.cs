using DealerBear.Card.Interface;
using DealerBear.Messages.Interface;

namespace DealerBear.Messages.Implementation
{
    public class RequestStartingCard : IRequestStartingCard
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }
        public float Seed { get; set; }
        public int PackVersionNumber { get; set; }
        public ICard CurrentCard { get; set; }
    }
}