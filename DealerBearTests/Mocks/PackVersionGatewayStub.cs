using DealerBear.Gateway.Interface;

namespace DealerBearTests.Mocks
{
    public class PackVersionGatewayStub : IPackVersionGateway
    {
        private readonly int _returnGetCurrentPackVersion;

        public PackVersionGatewayStub(int returnGetCurrentPackVersion)
        {
            _returnGetCurrentPackVersion = returnGetCurrentPackVersion;
        }

        public int GetCurrentPackVersion() => _returnGetCurrentPackVersion;

        public void SetCurrentPackVersion(int value)
        {
        }
    }
}