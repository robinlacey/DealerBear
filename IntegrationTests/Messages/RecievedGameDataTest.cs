using Messages;

namespace IntegrationTests.Messages
{
    public class RecievedGameDataTest : IGameData
    {
        public string SessionID { get; set; }
        public ICard CurrentCard { get; set; }
    }
}