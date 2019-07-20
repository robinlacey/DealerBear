using System;
using System.Threading.Tasks;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using DealerBear.UseCases.GetCurrentGameState.Interface;
using MassTransit;

namespace DealerBear.Consumers
{
    public class RequestGameSessionNotFoundConsumer : IConsumer<IRequestGameSessionNotFound>
    {
        private readonly IGameSessionNotFound _gameSessionNotFoundUseCase;
        private readonly ICreateGameState _createGameStateUseCase;
        private readonly IAwaitingResponseGateway _awaitingResponseGateway;


        public RequestGameSessionNotFoundConsumer(
            IGameSessionNotFound gameSessionNotFoundUseCase,
            ICreateGameState createGameStateUseCase, 
            IAwaitingResponseGateway awaitingResponseGateway)
        
        {
            _gameSessionNotFoundUseCase = gameSessionNotFoundUseCase;
            _createGameStateUseCase = createGameStateUseCase;
            _awaitingResponseGateway = awaitingResponseGateway;
        }

        public async Task Consume(ConsumeContext<IRequestGameSessionNotFound> context)
        {
            _gameSessionNotFoundUseCase.Execute(context.Message,_createGameStateUseCase, _awaitingResponseGateway, context);
        }
    }
}