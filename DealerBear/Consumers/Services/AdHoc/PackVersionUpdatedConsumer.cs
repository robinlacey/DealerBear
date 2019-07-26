using System.Threading.Tasks;
using DealerBear.Gateway.Interface;
using DealerBear.Messages.Interface;
using MassTransit;

namespace DealerBear.Consumers.Services.AdHoc
{
    public class PackVersionUpdatedConsumer : IConsumer<IRequestPackVersionNumberUpdated>
    {
        private readonly IPackVersionGateway _gateway;

        public PackVersionUpdatedConsumer(IPackVersionGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task Consume(ConsumeContext<IRequestPackVersionNumberUpdated> context)
        {
            _gateway.SetCurrentPackVersion(context.Message.PackNumber);
        }
    }
}