using DealerBear.Gateway.Interface;

namespace DealerBearTests.Mocks
{
    public class PackVersionGatewaySpy : IPackVersionGateway
    {
        public int GetCurrentPackVersion() => 0;
        public int SetCurrentPackVersionValue { get; private set; }

        public void SetCurrentPackVersion(int value)
        {
            SetCurrentPackVersionValue = value;
        }
    }
}