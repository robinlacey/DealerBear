using Messages;

namespace IntegrationCore.Messages
{
    public class RecievedGameDataTest : IGameData
    {
        public string SessionID { get; set; }
        public ICard CurrentCard { get; set; }
    }
}