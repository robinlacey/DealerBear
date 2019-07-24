namespace DealerBear.Messages
{
    public interface IGameResponse
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
        // Current Card
    }
}