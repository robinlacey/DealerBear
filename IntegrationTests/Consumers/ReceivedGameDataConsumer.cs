using System;
using System.Threading.Tasks;
using IntegrationTests.Messages;
using MassTransit;
using Messages;

namespace IntegrationTests.Consumers
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