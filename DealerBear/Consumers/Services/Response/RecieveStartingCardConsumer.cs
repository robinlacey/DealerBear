using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.CreateNewGame.Interface;
using MassTransit;

namespace DealerBear.Consumers.Services.Response
{
    public class RecieveStartingCardConsumer:IConsumer<IStartingCardResponse>
    {
        private readonly ICreateNewGame _createNewGame;
        private readonly IAwaitingResponseGateway _responseGateway;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public RecieveStartingCardConsumer(ICreateNewGame createNewGame,IAwaitingResponseGateway responseGateway, IPublishMessageAdaptor publishMessageAdaptor)
        {
            _createNewGame = createNewGame;
            _responseGateway = responseGateway;
            _publishMessageAdaptor = publishMessageAdaptor;
        }
        public async Task Consume(ConsumeContext<IStartingCardResponse> context)
        {
            _createNewGame.Execute(
                context.Message.SessionID, 
                context.Message.MessageID, 
                context.Message.StartingCardID,
                context.Message.Seed, 
                context.Message.PackVersionNumber, 
                _responseGateway,_publishMessageAdaptor);
        }
    }
}