namespace DealerBear.Messages.Interface
{
    public interface IRequestGameSessionFound
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}