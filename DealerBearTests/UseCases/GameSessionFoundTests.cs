using DealerBear.Exceptions;
using DealerBear.UseCases.GameSessionFound;
using DealerBear.UseCases.GetGameInProgress.Interface;
using DealerBearTests.Mocks;
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
                    IGetGameInProgress getGameInProgressDummy = new GetGameInProgressDummy();
                    new GameSessionFound(getGameInProgressDummy,_awaitingResponseGatewaySpy).Execute("Session ID", messageID);

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Jumped On The Table And Ate Dinner")]
                [TestCase("Bad Dog")]
                public void ThenPopIDIsCalledOnAwaitingResponseGateway(string messageID)
                {
                    IGetGameInProgress getGameInProgressDummy = new GetGameInProgressDummy();
                    new GameSessionFound(getGameInProgressDummy,_awaitingResponseGatewaySpy).Execute("SessionID", messageID);

                    Assert.True(_awaitingResponseGatewaySpy.PopIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Barked At The Ticket Inspector")]
                [TestCase("Bad Dog")]
                public void ThenGetGameStateGameUseCaseIsExecuted(string messageID)
                {
                    GetGameInProgressSpy spy = new GetGameInProgressSpy();
                    new GameSessionFound(spy,_awaitingResponseGatewaySpy).Execute("SessionID", messageID);

                    Assert.True(spy.ExecuteCalled);
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
                   
                    IGetGameInProgress getGameInProgressDummy = new GetGameInProgressDummy();
                    new GameSessionFound(getGameInProgressDummy, _awaitingResponseGatewaySpy).Execute("SessionID",messageID);

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [Test]
                public void ThenGetGameStateGameUseCaseIsNotExecuted()
                {
                    GetGameInProgressSpy getGameInProgressSpy = new GetGameInProgressSpy();
                    new GameSessionFound(getGameInProgressSpy, _awaitingResponseGatewaySpy).Execute("Session ID", "Message ID");
                    Assert.False(getGameInProgressSpy.ExecuteCalled);
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
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);
                    IGetGameInProgress getGameInProgressDummy = new GetGameInProgressDummy();

                    Assert.Throws<InvalidSessionIDException>(() =>
                        new GameSessionFound(getGameInProgressDummy, spy).Execute(invalidInput, "Message ID"));
                }
            }

            public class WhenMessageIDIsInvalid
            {
                [TestCase("   ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidMessageIDException(string invalidInput)
                {
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);
                    IGetGameInProgress getGameInProgressDummy = new GetGameInProgressDummy();

                    Assert.Throws<InvalidMessageIDException>(() =>
                        new GameSessionFound(getGameInProgressDummy, spy).Execute("Session ID", invalidInput));
                }
            }
        }
    }
}