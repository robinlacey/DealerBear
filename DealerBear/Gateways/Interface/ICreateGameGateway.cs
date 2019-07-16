namespace DealerBear.Gateways.Interface
{
    public interface ICreateGameGateway
    {
        string CreateGame(string gameName, string sessionID);
    }
}