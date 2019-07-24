using System.Threading.Tasks;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.GameSessionFound.Interface;
using DealerBear.UseCases.GetGameInProgress.Interface;
using MassTransit;

namespace DealerBear.Consumers.Services.Response
{
    public class GameSessionFoundConsumer : IConsumer<IRequestGameSessionFound>
    {
        private readonly IGameSessionFound _gameSessionFoundUseCase;
        private readonly IGetGameInProgress _getGameInProgressUseCase;
        private readonly IAwaitingResponseGateway _awaitingResponseGateway;

        public GameSessionFoundConsumer(
            IGameSessionFound gameSessionFoundUseCase,
            IGetGameInProgress getGameInProgressUseCase,
            IAwaitingResponseGateway awaitingResponseGateway)
        {
            _gameSessionFoundUseCase = gameSessionFoundUseCase;
            _getGameInProgressUseCase = getGameInProgressUseCase;
            _awaitingResponseGateway = awaitingResponseGateway;
        }

        public async Task Consume(ConsumeContext<IRequestGameSessionFound> context)
        {
            // This is where you could add an additional "Welcome Back" response 
            _gameSessionFoundUseCase.Execute(context.Message, _getGameInProgressUseCase, _awaitingResponseGateway,
                context);
        }
    }
}