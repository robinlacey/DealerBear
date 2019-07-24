namespace DealerBear.Messages
{
    public interface IGameSessionNotFoundRequest
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}