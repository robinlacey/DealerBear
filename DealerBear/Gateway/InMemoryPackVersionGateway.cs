using DealerBear.Gateway.Interface;

namespace DealerBear.Gateway
{
    public class InMemoryPackVersionGateway : IPackVersionGateway
    {
        private int packVersion;
        public int GetCurrentPackVersion() => packVersion;

        public void SetCurrentPackVersion(int value) => packVersion = value;
    }
}