using Messages;

namespace IntegrationTests.Messages
{
    public class StartGameRequestMessage : IGameRequest
    {
        public string SessionID { get; set; } = "Scout The Dog";
    }
}