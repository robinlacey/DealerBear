using DealerBear.Exceptions;
using DealerBear.Messages;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBear.UseCases.GameSessionNotFound;
using DealerBear.UseCases.GameSessionNotFound.Interface;
using DealerBearTests.Mocks;
using MassTransit;
using NUnit.Framework;

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
                    IRequestGameSessionNotFound message = new RequestGameSessionNotFoundStub("Session ID", messageID);

                    gameSessionFoundUseCase.Execute(message,
                        new CreateGameStateDummy(), _awaitingResponseGatewaySpy, new PackVersionGatewayDummy(),
                        new GenerateSeedDummy(), new PublishEndPointDummy());

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Jumped On The Table And Ate Dinner")]
                [TestCase("Bad Dog")]
                public void ThenPopIDIsCalledOnAwaitingResponseGateway(string messageID)
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IRequestGameSessionNotFound message = new RequestGameSessionNotFoundStub("Session ID", messageID);


                    gameSessionFoundUseCase.Execute(message, new CreateGameStateDummy(),
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
                    IRequestGameSessionNotFound message = new RequestGameSessionNotFoundStub("Session ID", messageID);

                    CreateGameStateSpy createGameStateSpy = new CreateGameStateSpy();

                    gameSessionFoundUseCase.Execute(message,
                        createGameStateSpy, _awaitingResponseGatewaySpy, new PackVersionGatewayDummy(),
                        new GenerateSeedDummy(), new PublishEndPointDummy());

                    Assert.True(createGameStateSpy.ExecuteCalled);
                }
            }

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
                    IRequestGameSessionNotFound message = new RequestGameSessionNotFoundStub("Session ID", messageID);
                    ICreateGameState createGameStateDummy = new CreateGameStateDummy();

                    gameSessionFoundUseCase.Execute(message, createGameStateDummy,
                        _awaitingResponseGatewaySpy, new PackVersionGatewayDummy(), new GenerateSeedDummy(),
                        new PublishEndPointDummy());

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [Test]
                public void ThenGetGameStateGameUseCaseIsNotExecuted()
                {
                    IGameSessionNotFound gameSessionFoundUseCase = new GameSessionNotFound();
                    IRequestGameSessionNotFound
                        message = new RequestGameSessionNotFoundStub("Session ID", "Message ID");
                    CreateGameStateSpy getCurrentGameStateSpy = new CreateGameStateSpy();

                    gameSessionFoundUseCase.Execute(message, getCurrentGameStateSpy,
                        new AwaitingResponseGatewayDummy(), new PackVersionGatewayDummy(), new GenerateSeedDummy(),
                        new PublishEndPointDummy());

                    Assert.False(getCurrentGameStateSpy.ExecuteCalled);
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
                    IRequestGameSessionNotFound
                        message = new RequestGameSessionNotFoundStub(invalidInput, "Message ID");
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);

                    Assert.Throws<InvalidSessionIDException>(() => gameSessionFoundUseCase.Execute(
                        message, new CreateGameStateDummy(), spy, new PackVersionGatewayDummy(),
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
                    IRequestGameSessionNotFound
                        message = new RequestGameSessionNotFoundStub("Session ID", invalidInput);
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);
                    ICreateGameState createGameStateDummy = new CreateGameStateDummy();
                    IPublishEndpoint publishEndPointDummy = new PublishEndPointDummy();

                    Assert.Throws<InvalidMessageIDException>(() => gameSessionFoundUseCase.Execute(
                        message, createGameStateDummy, spy,
                        new PackVersionGatewayDummy(), new GenerateSeedDummy(), new PublishEndPointDummy()));
                }
            }
        }
    }
}