using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages.Implementation;
using DealerBear.UseCases.CheckIfGameInProgress.Interface;

namespace DealerBear.UseCases.CheckIfGameInProgress
{
    public class CheckIfGameInProgress : ICheckIfGameInProgress
    {
        private readonly IAwaitingResponseGateway _responseGateway;
        private readonly IPublishMessageAdaptor _publishEndPoint;

        public CheckIfGameInProgress(IAwaitingResponseGateway responseGateway,
            IPublishMessageAdaptor publishEndPoint)
        {
            _responseGateway = responseGateway;
            _publishEndPoint = publishEndPoint;
        }
        public void Execute(string sessionID)
        {
            if (InvalidIDString(sessionID))
            {
                throw new InvalidSessionIDException();
            }

            string messageID = Guid.NewGuid().ToString();
            _responseGateway.SaveID(messageID);
            _publishEndPoint.Publish(new RequestGameIsSessionIDInUse
            {
                SessionID = sessionID,
                MessageID = messageID
            });
        }
        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}