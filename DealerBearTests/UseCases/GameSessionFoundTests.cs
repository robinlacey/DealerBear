using DealerBear.Exceptions;
using DealerBear.Messages;
using DealerBear.UseCases.GameSessionFound;
using DealerBear.UseCases.GameSessionFound.Interface;
using DealerBear.UseCases.GetCurrentGameState.Interface;
using DealerBearTests.Mocks;
using MassTransit;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class GameSessionFoundTests
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
                    IGameSessionFound gameSessionFoundUseCase = new GameSessionFound();
                    IRequestGameSessionFound message = new RequestGameSessionFoundStub("Session ID", messageID);

                    IGetCurrentGameState getCurrentGameStateDummy = new GetCurrentGameStateDummy();
                    IPublishEndpoint publishEndPointDummy = new PublishEndPointDummy();

                    gameSessionFoundUseCase.Execute(message, getCurrentGameStateDummy, _awaitingResponseGatewaySpy,
                        publishEndPointDummy);

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Jumped On The Table And Ate Dinner")]
                [TestCase("Bad Dog")]
                public void ThenPopIDIsCalledOnAwaitingResponseGateway(string messageID)
                {
                    IGameSessionFound gameSessionFoundUseCase = new GameSessionFound();
                    IRequestGameSessionFound message = new RequestGameSessionFoundStub("Session ID", messageID);

                    IGetCurrentGameState getCurrentGameStateDummy = new GetCurrentGameStateDummy();
                    IPublishEndpoint publishEndPointDummy = new PublishEndPointDummy();

                    gameSessionFoundUseCase.Execute(message, getCurrentGameStateDummy, _awaitingResponseGatewaySpy,
                        publishEndPointDummy);

                    Assert.True(_awaitingResponseGatewaySpy.PopIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Barked At The Ticket Inspector")]
                [TestCase("Bad Dog")]
                public void ThenGetGameStateGameUseCaseIsExecuted(string messageID)
                {
                    IGameSessionFound gameSessionFoundUseCase = new GameSessionFound();
                    IRequestGameSessionFound message = new RequestGameSessionFoundStub("Session ID", messageID);

                    GetCurrentGameStateSpy getCurrentGameStateDummy = new GetCurrentGameStateSpy();
                    IPublishEndpoint publishEndPointDummy = new PublishEndPointDummy();

                    gameSessionFoundUseCase.Execute(message, getCurrentGameStateDummy, _awaitingResponseGatewaySpy,
                        publishEndPointDummy);

                    Assert.True(getCurrentGameStateDummy.ExecuteCalled);
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
                    IGameSessionFound gameSessionFoundUseCase = new GameSessionFound();
                    IRequestGameSessionFound message = new RequestGameSessionFoundStub("Session ID", messageID);
                    IGetCurrentGameState getCurrentGameStateDummy = new GetCurrentGameStateDummy();
                    IPublishEndpoint publishEndPointDummy = new PublishEndPointDummy();

                    gameSessionFoundUseCase.Execute(message, getCurrentGameStateDummy, _awaitingResponseGatewaySpy,
                        publishEndPointDummy);

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [Test]
                public void ThenGetGameStateGameUseCaseIsNotExecuted()
                {
                    IGameSessionFound gameSessionFoundUseCase = new GameSessionFound();
                    IRequestGameSessionFound message = new RequestGameSessionFoundStub("Session ID", "Message ID");
                    GetCurrentGameStateSpy getCurrentGameStateSpy = new GetCurrentGameStateSpy();
                    IPublishEndpoint publishEndPointDummy = new PublishEndPointDummy();

                    gameSessionFoundUseCase.Execute(message, getCurrentGameStateSpy, _awaitingResponseGatewaySpy,
                        publishEndPointDummy);

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
                    IGameSessionFound gameSessionFoundUseCase = new GameSessionFound();
                    IRequestGameSessionFound message = new RequestGameSessionFoundStub(invalidInput, "Message ID");
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);
                    IGetCurrentGameState getCurrentGameStateDummy = new GetCurrentGameStateDummy();
                    IPublishEndpoint publishEndPointDummy = new PublishEndPointDummy();

                    Assert.Throws<InvalidSessionIDException>(() =>
                        gameSessionFoundUseCase.Execute(message, getCurrentGameStateDummy, spy, publishEndPointDummy));
                }
            }

            public class WhenMessageIDIsInvalid
            {
                [TestCase("   ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidMessageIDException(string invalidInput)
                {
                    IGameSessionFound gameSessionFoundUseCase = new GameSessionFound();
                    IRequestGameSessionFound message = new RequestGameSessionFoundStub("Session ID", invalidInput);
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);
                    IGetCurrentGameState getCurrentGameStateDummy = new GetCurrentGameStateDummy();
                    IPublishEndpoint publishEndPointDummy = new PublishEndPointDummy();

                    Assert.Throws<InvalidMessageIDException>(() =>
                        gameSessionFoundUseCase.Execute(message, getCurrentGameStateDummy, spy, publishEndPointDummy));
                }
            }
        }
    }
}