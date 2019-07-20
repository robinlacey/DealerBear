using DealerBear.Gateway.Interface;

namespace DealerBearTests.Mocks
{
    public class AwaitingResponseGatewaySpy:IAwaitingResponseGateway
    {       
        public string HasUIDInput { get; private set; }
        public string PopUIDInput { get; private set; }
        private readonly bool _hasUiDreturn;

        public AwaitingResponseGatewaySpy(bool hasUIDreturn)
        {
            _hasUiDreturn = hasUIDreturn;
        }

        public bool HasID(string uid)
        {
            HasUIDInput = uid;
            return _hasUiDreturn;
        }

        public void PopID(string uid)
        {
            PopUIDInput = uid;
        }
    }
}