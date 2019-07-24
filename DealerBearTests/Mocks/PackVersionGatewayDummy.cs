using DealerBear.Gateway.Interface;

namespace DealerBearTests.Mocks
{
    public class PackVersionGatewayDummy : IPackVersionGateway
    {
        public int GetCurrentPackVersion() => 0;

        public void SetCurrentPackVersion(int value)
        {
        }
    }
}