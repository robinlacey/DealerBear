using Messages;

namespace DealerBear.Messages
{
    public class TestGameData : IGameData
    {
        public string SessionID { get; set; }
        public ICard CurrentCard { get; set; }
    }
}