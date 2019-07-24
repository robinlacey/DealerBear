namespace DealerBear.Messages
{
    public interface ICreateNewGameData
    {
        string SessionID { get; }
        string MessageID { get; }
        float Seed { get; }
        int PackVersionNumber { get; }
    }
}