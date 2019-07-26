using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.Messages.Implementation;
using DealerBear.UseCases.GetGameInProgress.Interface;

namespace DealerBear.UseCases.GetGameInProgress
{
    public class GetGameInProgress : IGetGameInProgress
    {
        private readonly IAwaitingResponseGateway _awaitingResponseGateway;
        private readonly IPublishMessageAdaptor _publishEndPoint;

        public GetGameInProgress(
            IAwaitingResponseGateway awaitingResponseGateway,
            IPublishMessageAdaptor publishEndPoint)
        {
            _awaitingResponseGateway = awaitingResponseGateway;
            _publishEndPoint = publishEndPoint;
        }
        public void Execute(string sessionID)
        {
            if (InvalidIDString(sessionID))
            {
                throw new InvalidSessionIDException();
            }
            string messageID = Guid.NewGuid().ToString();
            _publishEndPoint.Publish(new GetCurrentGameData
            {
                SessionID = sessionID,
                MessageID = messageID
            });
            _awaitingResponseGateway.SaveID(messageID);
        }
        
        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}