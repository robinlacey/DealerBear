using Messages;

namespace DealerBear_API.Messages
{
    public class TestGameData : IGameData
    {
        public string SessionID { get; set; }
        public ICard CurrentCard { get; set; }
    }
}