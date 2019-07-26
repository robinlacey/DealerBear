using DealerBear.Messages.Interface;

namespace DealerBear.Messages.Implementation
{
    public class GetCurrentGameData:IGetGameData
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }
    }
}