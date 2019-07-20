using System;
using System.Threading.Tasks;
using GameBear.Gateways.Interface;
using GameBear.UseCases.RequestGameCheckExistingSession;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
using MassTransit;
using Messages;

namespace GameBear.Consumers
{
    public class IsExistingSessionConsumer : IConsumer<IRequestGameIsSessionIDInUse>
    {
        private readonly IGameDataGateway _gameDataGateway;

        public IsExistingSessionConsumer(IGameDataGateway gameDataGateway)
        {
            Console.WriteLine("DataGateWay is null?" + (gameDataGateway == null));
            Console.WriteLine("DataGateWay type" + gameDataGateway.GetType());
            _gameDataGateway = gameDataGateway;
        }

        public async Task Consume(ConsumeContext<IRequestGameIsSessionIDInUse> context)
        {
            IRequestGameCheckExistingSession useCase = new RequestGameCheckExistingSession();
            Console.WriteLine("Recieved IRequestGameIsSessionIDInUse");
            useCase.Execute(context.Message, _gameDataGateway, context);
        }
    }
}