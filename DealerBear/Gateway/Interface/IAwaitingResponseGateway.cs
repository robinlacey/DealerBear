namespace DealerBear.Gateway.Interface
{
    public interface IAwaitingResponseGateway
    {
        bool HasID(string uid);
        void PopID(string uid);
        void SaveID(string uid);
    }
}