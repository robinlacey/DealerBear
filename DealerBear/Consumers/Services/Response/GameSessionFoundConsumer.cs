using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.GameSessionFound.Interface;
using DealerBear.UseCases.GetGameInProgress.Interface;
using MassTransit;

namespace DealerBear.Consumers.Services.Response
{
    public class GameSessionFoundConsumer : IConsumer<IRequestGameSessionFound>
    {
        private readonly IGameSessionFound _gameSessionFoundUseCase;

        public GameSessionFoundConsumer(
            IGameSessionFound gameSessionFoundUseCase)
        {
            _gameSessionFoundUseCase = gameSessionFoundUseCase;
        }

        public async Task Consume(ConsumeContext<IRequestGameSessionFound> context)
        {
            // This is where you could add an additional "Welcome Back" response 
            _gameSessionFoundUseCase.Execute(context.Message.SessionID, context.Message.MessageID);
        }
    }
}