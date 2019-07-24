using System.Threading;
using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;

namespace DealerBearTests.Mocks
{
    public class PublishEndPointDummy : IPublishMessageAdaptor
    {
        public Task Publish<T>(T message, CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            return null;
        }

    }
}