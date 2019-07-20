using System;
using System.Threading.Tasks;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using MassTransit;
using Messages;

namespace DealerBear.Consumers
{
    public class RequestGameSessionNotFoundConsumer:IConsumer<IRequestGameSessionNotFound>
    {
        private readonly IGameSessionNotFound _gameSessionNotFoundUseCase;

        public RequestGameSessionNotFoundConsumer(IGameSessionNotFound gameSessionNotFoundUseCase)
        {
            Console.WriteLine("HELLOWRequestGameSessionNotFoundConsumer");
            _gameSessionNotFoundUseCase = gameSessionNotFoundUseCase;
        }
        public async Task Consume(ConsumeContext<IRequestGameSessionNotFound> context)
        {
            Console.WriteLine("NOT FOUND");
            _gameSessionNotFoundUseCase.Execute(context.Message,context);
        }
    }
}