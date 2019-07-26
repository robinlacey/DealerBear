namespace DealerBear.UseCases.GameSessionFound.Interface
{
    public interface IGameSessionFound
    {
        void Execute(string sessionID, string messageID);
    }
}