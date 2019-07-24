using System.Threading.Tasks;
using DealerBear.Messages;
using MassTransit;

namespace DealerBear.Consumers.Player.Response
{
    public class ReceiveGameConsumer: IConsumer<IGameResponse>
    {
        public async Task Consume(ConsumeContext<IGameResponse> context)
        {
            // TODO 
            // Publish message with data to Player via UseCase
        }
    }
}