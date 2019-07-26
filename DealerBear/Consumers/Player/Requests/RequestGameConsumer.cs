using System.Threading.Tasks;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.CheckIfGameInProgress.Interface;
using MassTransit;

namespace DealerBear.Consumers.Player.Requests
{
    public class RequestGameConsumer : IConsumer<IGameRequest>
    {
        private readonly ICheckIfGameInProgress _checkIfGameInProgressUseCase;
        public RequestGameConsumer(
            ICheckIfGameInProgress checkIfGameInProgressUseCase)
        {
            _checkIfGameInProgressUseCase = checkIfGameInProgressUseCase;
        }

        public async Task Consume(ConsumeContext<IGameRequest> context)
        {
            _checkIfGameInProgressUseCase.Execute(context.Message.SessionID);
        }
    }
}