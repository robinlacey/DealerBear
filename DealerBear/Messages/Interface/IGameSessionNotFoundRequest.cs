namespace DealerBear.Messages.Interface
{
    public interface IGameSessionNotFoundRequest
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}