using DealerBear.Messages.Interface;

namespace DealerBear.Messages.Implementation
{
    public class CreateNewGameRequest:ICreateNewGameRequest
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }
        public string StartingCardID { get;set;  }
        public int Seed { get; set; }
        public int PackVersionNumber { get; set; }
    }
}