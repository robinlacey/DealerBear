namespace DealerBear.Messages.Interface
{
    public interface IGetGameData
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}