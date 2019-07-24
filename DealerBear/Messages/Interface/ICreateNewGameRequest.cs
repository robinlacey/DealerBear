namespace DealerBear.Messages.Interface
{
    public interface ICreateNewGameRequest
    {
        string SessionID { get; }
        string MessageID { get; }
        string StartingCardID { get; }
        int Seed { get; }
        int PackVersionNumber { get; }
    }
}