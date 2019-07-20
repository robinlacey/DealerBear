using DealerBear.Gateway.Interface;

namespace DealerBearTests.Mocks
{
    public class AwaitingResponseGatewayDummy : IAwaitingResponseGateway
    {
        public bool HasID(string uid) => false;

        public void PopID(string uid)
        {
        }

        public void SaveID(string uid)
        {
        }
    }
}