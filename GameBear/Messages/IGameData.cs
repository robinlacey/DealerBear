namespace Messages
{
    public interface IGameData
    {
        string SessionID { get; set; }
        ICard CurrentCard { get; set; }
    }
}