using DealerBear_API.Gateways.Interface;

namespace DealerBearTests.Mocks.Gateways
{
    public class SessionsGatewayDummy:ISessionsGateway
    {
        public bool IsActiveSession(string sessionID) => false;
    }
}