using DealerBear.Gateway.Interface;

namespace DealerBearTests.Mocks
{
    public class AwaitingResponseGatewaySpy : IAwaitingResponseGateway
    {
        public string HasIDInput { get; private set; }
        public string PopIDInput { get; private set; }
        public string SaveIDInput { get; private set; }
        private readonly bool _hasUiDreturn;

        public AwaitingResponseGatewaySpy(bool hasUIDreturn)
        {
            _hasUiDreturn = hasUIDreturn;
        }

        public bool HasID(string uid)
        {
            HasIDInput = uid;
            return _hasUiDreturn;
        }

        public void PopID(string uid)
        {
            PopIDInput = uid;
        }

        public void SaveID(string uid)
        {
            SaveIDInput = uid;
        }
    }
}