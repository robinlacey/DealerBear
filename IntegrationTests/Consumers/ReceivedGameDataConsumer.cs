using System.Threading.Tasks;
using IntegrationCore.Messages;
using MassTransit;
using Messages;

namespace IntegrationCore.Consumers
{
    public class ReceivedGameDataConsumer : IConsumer<IGameData>
    {
        public async Task Consume(ConsumeContext<IGameData> context)
        {
            AcceptanceTestStages.ReturnedCorrectStartGameCardID =
                new StartGameRequestMessage().SessionID == context.Message.SessionID;
        }
    }
}