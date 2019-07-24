using System.Threading;
using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;
using MassTransit;

namespace DealerBear.Adaptor
{
    public class PublishMessageMassTransitAdaptor:IPublishMessageAdaptor
    {
        private readonly IPublishEndpoint _massTransitEndPoint;

        public PublishMessageMassTransitAdaptor(IPublishEndpoint massTransitEndPoint)
        {
            _massTransitEndPoint = massTransitEndPoint;
        }
        public Task Publish<T>(T message, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            return _massTransitEndPoint.Publish(message, cancellationToken);
        }
    }
}