using DealerBear_API.Gateways.Interface;

namespace DealerBearTests.Mocks.Gateways
{
    public class SessionsGatewayStub:ISessionsGateway
    {
        private readonly bool _isActiveSessionReturn;

        public SessionsGatewayStub(bool isActiveSessionReturn)
        {
            _isActiveSessionReturn = isActiveSessionReturn;
        }

        public bool IsActiveSession(string sessionID) => _isActiveSessionReturn;
    }
}