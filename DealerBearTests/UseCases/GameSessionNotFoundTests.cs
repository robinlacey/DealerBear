using DealerBear.Exceptions;
using DealerBear.Messages;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.GameSessionNotFound;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using DealerBearTests.Mocks;
using NUnit.Framework;
using IGetStartingCard = DealerBear.UseCases.GetStartingCard.Interface.IGetStartingCard;

namespace DealerBearTests.UseCases
{
    public class GameSessionNotFoundTests
    {
        public class GivenValidInput
        {
            public class WhenMessageIDIsInAwaitingResponseGateway
            {
                private AwaitingResponseGatewaySpy _awaitingResponseGatewaySpy;

                [SetUp]
                public void Setup()
                {
                    _awaitingResponseGatewaySpy = new AwaitingResponseGatewaySpy(true);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Bit The Postman")]
                [TestCase("Bad Dog")]
                public void ThenHasIDIsCalledOnAwaitingResponseGateway(string messageID)
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IGameSessionNotFoundRequest message = new GameSessionNotFoundRequestStub("Session ID", messageID);

                    gameSessionFoundUseCase.Execute(message,
                        new GetStartingCardDummy(), _awaitingResponseGatewaySpy, new PackVersionGatewayDummy(),
                        new GenerateSeedDummy(), new PublishEndPointDummy());

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Jumped On The Table And Ate Dinner")]
                [TestCase("Bad Dog")]
                public void ThenPopIDIsCalledOnAwaitingResponseGateway(string messageID)
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IGameSessionNotFoundRequest message = new GameSessionNotFoundRequestStub("Session ID", messageID);


                    gameSessionFoundUseCase.Execute(message, new GetStartingCardDummy(),
                        _awaitingResponseGatewaySpy, new PackVersionGatewayDummy(), new GenerateSeedDummy(),
                        new PublishEndPointDummy());

                    Assert.True(_awaitingResponseGatewaySpy.PopIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Barked At The Ticket Inspector")]
                [TestCase("Bad Dog")]
                public void ThenGetGameStateGameUseCaseIsExecuted(string messageID)
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IGameSessionNotFoundRequest message = new GameSessionNotFoundRequestStub("Session ID", messageID);

                    GetStartingCardSpy getStartingCardSpy = new GetStartingCardSpy();

                    gameSessionFoundUseCase.Execute(message,
                        getStartingCardSpy, _awaitingResponseGatewaySpy, new PackVersionGatewayDummy(),
                        new GenerateSeedDummy(), new PublishEndPointDummy());

                    Assert.True(getStartingCardSpy.ExecuteCalled);
                }
            }

            // Idempotent
            public class WhenMessageIDIsNotInAwaitingResponseGateway
            {
                private AwaitingResponseGatewaySpy _awaitingResponseGatewaySpy;

                [SetUp]
                public void Setup()
                {
                    _awaitingResponseGatewaySpy = new AwaitingResponseGatewaySpy(false);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Ate All The Bagels")]
                [TestCase("Bad Dog")]
                public void ThenHasIDIsCalledOnAwaitingResponseGateway(string messageID)
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IGameSessionNotFoundRequest message = new GameSessionNotFoundRequestStub("Session ID", messageID);
                    IGetStartingCard getStartingCardDummy = new GetStartingCardDummy();

                    gameSessionFoundUseCase.Execute(message, getStartingCardDummy,
                        _awaitingResponseGatewaySpy, new PackVersionGatewayDummy(), new GenerateSeedDummy(),
                        new PublishEndPointDummy());

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [Test]
                public void ThenGetGameStateGameUseCaseIsNotExecuted()
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IGameSessionNotFoundRequest
                        message = new GameSessionNotFoundRequestStub("Session ID", "Message ID");
                    GetStartingCardSpy getCurrentNewGameSpy = new GetStartingCardSpy();

                    gameSessionFoundUseCase.Execute(message, getCurrentNewGameSpy,
                        new AwaitingResponseGatewayDummy(), new PackVersionGatewayDummy(), new GenerateSeedDummy(),
                        new PublishEndPointDummy());

                    Assert.False(getCurrentNewGameSpy.ExecuteCalled);
                }
                
                [Test]
                public void ThenNewMessageIDIsCreatedAndAddedToAwaitingResponseGateway()
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IGameSessionNotFoundRequest
                        message = new GameSessionNotFoundRequestStub("Session ID", "Message ID");
                    GetStartingCardSpy getCurrentNewGameSpy = new GetStartingCardSpy();

                    gameSessionFoundUseCase.Execute(message, getCurrentNewGameSpy,
                        new AwaitingResponseGatewayDummy(), new PackVersionGatewayDummy(), new GenerateSeedDummy(),
                        new PublishEndPointDummy());

                    Assert.False(getCurrentNewGameSpy.ExecuteCalled);
                }
                
            }
        }

        public class GivenInvalidInput
        {
            public class WhenSessionIDIsInvalid
            {
                [TestCase("   ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidSessionIDException(string invalidInput)
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IGameSessionNotFoundRequest
                        message = new GameSessionNotFoundRequestStub(invalidInput, "Message ID");
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);

                    Assert.Throws<InvalidSessionIDException>(() => gameSessionFoundUseCase.Execute(
                        message, new GetStartingCardDummy(), spy, new PackVersionGatewayDummy(),
                        new GenerateSeedDummy(), new PublishEndPointDummy()));
                }
            }

            public class WhenMessageIDIsInvalid
            {
                [TestCase("   ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidMessageIDException(string invalidInput)
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IGameSessionNotFoundRequest
                        message = new GameSessionNotFoundRequestStub("Session ID", invalidInput);
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);
                    IGetStartingCard getStartingCardDummy = new GetStartingCardDummy();

                    Assert.Throws<InvalidMessageIDException>(() => gameSessionFoundUseCase.Execute(
                        message, getStartingCardDummy, spy,
                        new PackVersionGatewayDummy(), new GenerateSeedDummy(), new PublishEndPointDummy()));
                }
            }
        }
    }
}