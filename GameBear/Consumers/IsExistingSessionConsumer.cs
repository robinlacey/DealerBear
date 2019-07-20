using System;
using System.Threading.Tasks;
using GameBear.Gateways.Interface;
using GameBear.UseCases;
using GameBear.UseCases.IsExistingSession;
using GameBear.UseCases.IsExistingSession.Interface;
using MassTransit;
using Messages;

namespace GameBear.Consumers
{
    public class IsExistingSessionConsumer : IConsumer<IIsSessionIDInUse>
    {
        private readonly IGameDataGateway _gameDataGateway;

        public IsExistingSessionConsumer(IGameDataGateway gameDataGateway)
        {
            Console.WriteLine("DataGateWay is null?" + (gameDataGateway == null));
            Console.WriteLine("DataGateWay type" + gameDataGateway.GetType());
            _gameDataGateway = gameDataGateway;
        }
        public async Task Consume(ConsumeContext<IIsSessionIDInUse> context)
        {
            IIsExistingSession useCase = new IsExistingSession();
            useCase.Execute(context.Message,_gameDataGateway, context);
        }
    }
}