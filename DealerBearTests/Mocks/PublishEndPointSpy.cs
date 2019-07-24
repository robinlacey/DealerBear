using System;
using System.Threading;
using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;

namespace DealerBearTests.Mocks
{
    public class PublishEndPointSpy : IPublishMessageAdaptor
    {
        public Type TypeOfObjectPublished { get; private set; }
        public object MessageObject { get; private set; }
        
        public bool PublishRun { get; set; }
        public Task Publish<T>(T message, CancellationToken cancellationToken = new CancellationToken()) where T : class
        {
            PublishRun = true;
            MessageObject = message;
            TypeOfObjectPublished = message.GetType();
            return null;
        }

    }
}