namespace DealerBear.Messages
{
    public interface IRequestGameSessionNotFound
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}