namespace DealerBear.Gateway.Interface
{
    public interface IPackVersionGateway
    {
        int GetCurrentPackVersion();
        void SetCurrentPackVersion(int value);
    }
}