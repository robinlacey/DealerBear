namespace DealerBear_API.Gateways.Interface
{
    public interface ISessionsGateway
    {
        bool IsActiveSession(string sessionID);
    }
}