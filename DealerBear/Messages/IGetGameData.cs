namespace DealerBear.Messages
{
    public interface IGetGameData
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}