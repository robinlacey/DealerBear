using System;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;

namespace GameBearTests.Mocks
{
    public class PublishEndPointSpy : IPublishEndpoint
    {
        public Type TypeOfObjectPublished { get; private set; }
        public object MessageObject { get; private set; }

        public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
        {
            throw new NotImplementedException();
        }

        public Task Publish<T>(T message, CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            MessageObject = message;
            TypeOfObjectPublished = message.GetType();
            return null;
        }

        public Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe,
            CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            MessageObject = message;
            TypeOfObjectPublished = message.GetType();
            return null;
        }

        public Task Publish<T>(T message, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            MessageObject = message;
            TypeOfObjectPublished = message.GetType();
            return null;
        }

        public Task Publish(object message, CancellationToken cancellationToken = new CancellationToken())
        {
            MessageObject = message;
            TypeOfObjectPublished = message.GetType();
            return null;
        }

        public Task Publish(object message, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken = new CancellationToken())
        {
            MessageObject = message;
            TypeOfObjectPublished = message.GetType();
            return null;
        }

        public Task Publish(object message, Type messageType,
            CancellationToken cancellationToken = new CancellationToken())
        {
            MessageObject = message;
            TypeOfObjectPublished = message.GetType();
            return null;
        }

        public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken = new CancellationToken())
        {
            MessageObject = message;
            TypeOfObjectPublished = message.GetType();
            return null;
        }

        public Task Publish<T>(object values, CancellationToken cancellationToken = new CancellationToken())
            where T : class
        {
            TypeOfObjectPublished = typeof(T);
            return null;
        }

        public Task Publish<T>(object values, IPipe<PublishContext<T>> publishPipe,
            CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            TypeOfObjectPublished = typeof(T);
            return null;
        }

        public Task Publish<T>(object values, IPipe<PublishContext> publishPipe,
            CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            TypeOfObjectPublished = typeof(T);
            return null;
        }
    }
}