namespace DealerBear.Messages
{
    public interface ICreateNewGameData
    {
        string SessionID { get; }
        float Seed { get; }
        int PackVersionNumber { get; }
    }
}