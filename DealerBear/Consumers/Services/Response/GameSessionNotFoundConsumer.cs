using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
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

        public GameSessionNotFoundConsumer(
            IGameSessionNotFound gameSessionNotFoundUseCase)
        {
            _gameSessionNotFoundUseCase = gameSessionNotFoundUseCase;
        }

        public async Task Consume(ConsumeContext<IGameSessionNotFoundRequest> context)
        {
            // This is where you could add an additional "How To Play" response 
            _gameSessionNotFoundUseCase.Execute(context.Message.SessionID,context.Message.MessageID);
        }
    }
}