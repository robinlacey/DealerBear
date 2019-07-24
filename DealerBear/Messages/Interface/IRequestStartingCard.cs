namespace DealerBear.Messages.Interface
{
    public interface IRequestStartingCard
    {
        string SessionID { get; }
        string MessageID { get; }
        float Seed { get; }
        int PackVersionNumber { get; }
    }
}