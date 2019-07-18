using Messages;

namespace IntegrationCore.Messages
{
    public class StartGameRequestMessage : IGameRequest
    {
        public string SessionID { get; set; } = "Scout The Dog";
    }
}