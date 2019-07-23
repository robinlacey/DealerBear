namespace DealerBear.Messages
{
    public interface IGameRequest
    {
        string SessionID { get; set; }
/*        int PackVersion { get; set; }
        float SessionSeed { get; set; }
        ICard CurrentCard { get; set; }
        IAddCard[] CardsToAdd { get; set; }*/
    }
}
// Thoughts on structure
/*namespace DealerBear.Messages
{
    public interface IAddCard
    {
        string CardIDToAdd { get; set; }
        float ProbabilityOfAdd { get; set; }
    }
}*/