using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using MassTransit;
using IGetStartingCard = DealerBear.UseCases.GetStartingCard.Interface.IGetStartingCard;

namespace DealerBear.Consumers.Services.Response
{
    public class GameSessionNotFoundConsumer : IConsumer<IGameSessionNotFoundRequest>
    {
        private readonly IGameSessionNotFound _gameSessionNotFoundUseCase;
        private readonly IGetStartingCard _getStartingCardUseCase;
        private readonly IAwaitingResponseGateway _awaitingResponseGateway;
        private readonly IPackVersionGateway _packVersionGateway;
        private readonly IGenerateSeed _generateSeed;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;


        public GameSessionNotFoundConsumer(
            IGameSessionNotFound gameSessionNotFoundUseCase,
            IGetStartingCard getStartingCardUseCase,
            IAwaitingResponseGateway awaitingResponseGateway,
            IPackVersionGateway packVersionGateway,
            IGenerateSeed generateSeed,
            IPublishMessageAdaptor publishMessageAdaptor)

        {
            _gameSessionNotFoundUseCase = gameSessionNotFoundUseCase;
            _getStartingCardUseCase = getStartingCardUseCase;
            _awaitingResponseGateway = awaitingResponseGateway;
            _packVersionGateway = packVersionGateway;
            _generateSeed = generateSeed;
            _publishMessageAdaptor = publishMessageAdaptor;
        }

        public async Task Consume(ConsumeContext<IGameSessionNotFoundRequest> context)
        {
            // This is where you could add an additional "How To Play" response 
            _gameSessionNotFoundUseCase.Execute(context.Message, _getStartingCardUseCase, _awaitingResponseGateway,
                _packVersionGateway, _generateSeed, _publishMessageAdaptor);
        }
    }
}