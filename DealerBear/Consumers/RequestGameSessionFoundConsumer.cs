using System;
using System.Threading.Tasks;
using DealerBear.Gateway.Interface;
using DealerBear.Messages;
using DealerBear.UseCases.GameSessionFound.Interface;
using DealerBear.UseCases.GetCurrentGameState.Interface;
using MassTransit;

namespace DealerBear.Consumers
{
    public class RequestGameSessionFoundConsumer : IConsumer<IRequestGameSessionFound>
    {
        private readonly IGameSessionFound _gameSessionFoundUseCase;
        private readonly IGetCurrentGameState _getCurrentGameStateUseCase;
        private readonly IAwaitingResponseGateway _awaitingResponseGateway;

        public RequestGameSessionFoundConsumer(
            IGameSessionFound gameSessionFoundUseCase,
            IGetCurrentGameState getCurrentGameStateUseCase, 
            IAwaitingResponseGateway awaitingResponseGateway)
        {
            _gameSessionFoundUseCase = gameSessionFoundUseCase;
            _getCurrentGameStateUseCase = getCurrentGameStateUseCase;
            _awaitingResponseGateway = awaitingResponseGateway;
        }

        public async Task Consume(ConsumeContext<IRequestGameSessionFound> context)
        {
            _gameSessionFoundUseCase.Execute(context.Message, _getCurrentGameStateUseCase, _awaitingResponseGateway,
                context);
        }
    }
}