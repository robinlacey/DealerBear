namespace DealerBear.Messages.Interface
{
    public interface IGameResponse
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
        // Current Card
    }
}