namespace Messages
{
    public interface IRequestGameIsSessionIDInUse
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
    }
}