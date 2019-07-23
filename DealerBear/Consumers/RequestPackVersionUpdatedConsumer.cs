using System.Threading.Tasks;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using MassTransit;

namespace DealerBear.Consumers
{
    public class RequestPackVersionUpdatedConsumer : IConsumer<IRequestPackNumberUpdated>
    {
        private readonly IPackVersionGateway _gateway;

        public RequestPackVersionUpdatedConsumer(IPackVersionGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task Consume(ConsumeContext<IRequestPackNumberUpdated> context)
        {
            _gateway.SetCurrentPackVersion(context.Message.PackNumber);
        }
    }
}