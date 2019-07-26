using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Exceptions;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using DealerBear.UseCases.GetStartingCard.Interface;

namespace DealerBear.UseCases.GetStartingCard
{
    public class GetStartingCard : IGetStartingCard
    {
        private readonly IPackVersionGateway _packVersionGateway;
        private readonly IAwaitingResponseGateway _awaitingResponseGateway;
        private readonly IGenerateSeed _generateSeedUseCase;
        private readonly IPublishMessageAdaptor _publishEndPoint;

        public GetStartingCard(
            IPackVersionGateway packVersionGateway,
            IAwaitingResponseGateway awaitingResponseGateway,
            IGenerateSeed generateSeedUseCase,
            IPublishMessageAdaptor publishEndPoint)
        {
            _packVersionGateway = packVersionGateway;
            _awaitingResponseGateway = awaitingResponseGateway;
            _generateSeedUseCase = generateSeedUseCase;
            _publishEndPoint = publishEndPoint;
        }
        public void Execute(string sessionID)
        {
            if (InvalidIDString(sessionID))
            {
                throw new InvalidSessionIDException();
            }

            string messageID = Guid.NewGuid().ToString();
            _publishEndPoint.Publish(new Messages.Implementation.RequestStartingCard
            {
                MessageID = messageID,
                SessionID = sessionID,
                PackVersionNumber = _packVersionGateway.GetCurrentPackVersion(),
                Seed = _generateSeedUseCase.Execute(),
            });
            _awaitingResponseGateway.SaveID(messageID);
        }


        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}