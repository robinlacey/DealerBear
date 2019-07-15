namespace DealerBear_API.UseCases.StartGame.Interface
{
    public interface IStartGame
    {
        string Execute(string gameName, string sessionID);
    }
}