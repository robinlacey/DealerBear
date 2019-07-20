using System;
using System.Threading.Tasks;
using DealerBear.UseCases.RequestGameSessionFound.Interface;
using MassTransit;
using Messages;

namespace DealerBear.Consumers
{
    public class RequestGameSessionFoundConsumer: IConsumer<IRequestGameSessionFound>
    {
        private readonly IGameSessionFound _gameSessionFoundUseCase;

        public RequestGameSessionFoundConsumer(IGameSessionFound gameSessionFoundUseCase)
        {
            Console.WriteLine("HeLLORequestGameSessionFoundConsumer");
            _gameSessionFoundUseCase = gameSessionFoundUseCase;
        }
        public async Task Consume(ConsumeContext<IRequestGameSessionFound> context)
        {
            Console.WriteLine("FOUND");
            _gameSessionFoundUseCase.Execute(context.Message,context);
        }
    }
}