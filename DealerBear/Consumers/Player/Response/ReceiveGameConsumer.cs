using System.Threading.Tasks;
using DealerBear.Messages.Interface;
using MassTransit;

namespace DealerBear.Consumers.Player.Response
{
    public class ReceiveGameConsumer: IConsumer<IGameResponse>
    {
        public async Task Consume(ConsumeContext<IGameResponse> context)
        {
            // TODO 
            // Get Card Data from Pack
            // Then send back to player 
        }
    }
}