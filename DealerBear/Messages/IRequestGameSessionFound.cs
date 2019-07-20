namespace DealerBear.Messages
{
    public interface IRequestGameSessionFound
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}