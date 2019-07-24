namespace DealerBear.Messages
{
    public class GetCurrentGameData:IGetGameData
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }
    }
}