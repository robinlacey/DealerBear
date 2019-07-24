namespace DealerBear.Messages.Interface
{
    public interface IRequestGameIsSessionIDInUse
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}