using System;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;

namespace GameBearTests.Mocks
{
    public class PublishEndPointDummy : IPublishEndpoint
    {
        public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
        {
            return null;
        }

        public Task Publish<T>(T message, CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            return null;
        }

        public Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe,
            CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            return null;
        }

        public Task Publish<T>(T message, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            return null;
        }

        public Task Publish(object message, CancellationToken cancellationToken = new CancellationToken())
        {
            return null;
        }

        public Task Publish(object message, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return null;
        }

        public Task Publish(object message, Type messageType,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return null;
        }

        public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return null;
        }

        public Task Publish<T>(object values, CancellationToken cancellationToken = new CancellationToken())
            where T : class
        {
            return null;
        }

        public Task Publish<T>(object values, IPipe<PublishContext<T>> publishPipe,
            CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            return null;
        }

        public Task Publish<T>(object values, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            return null;
        }
    }
}