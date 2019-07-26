using DealerBear.Exceptions;
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
                    new GameSessionNotFound(new GetStartingCardDummy(),_awaitingResponseGatewaySpy).Execute("Session ID", messageID);
                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Jumped On The Table And Ate Dinner")]
                [TestCase("Bad Dog")]
                public void ThenPopIDIsCalledOnAwaitingResponseGateway(string messageID)
                {
                    new GameSessionNotFound(new GetStartingCardDummy(),
                        _awaitingResponseGatewaySpy).Execute("Session ID", messageID);

                    Assert.True(_awaitingResponseGatewaySpy.PopIDInput == messageID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Barked At The Ticket Inspector")]
                [TestCase("Bad Dog")]
                public void ThenGetGameStateGameUseCaseIsExecuted(string messageID)
                {
                    GetStartingCardSpy getStartingCardSpy = new GetStartingCardSpy();
                    new GameSessionNotFound(getStartingCardSpy, _awaitingResponseGatewaySpy).Execute("Session ID", messageID);
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
                    IGetStartingCard getStartingCardDummy = new GetStartingCardDummy();

                    new GameSessionNotFound(getStartingCardDummy,
                        _awaitingResponseGatewaySpy).Execute("Session ID", messageID);

                    Assert.True(_awaitingResponseGatewaySpy.HasIDInput == messageID);
                }

                [Test]
                public void ThenGetGameStateGameUseCaseIsNotExecuted()
                {
                    GetStartingCardSpy getCurrentNewGameSpy = new GetStartingCardSpy();

                    new GameSessionNotFound(getCurrentNewGameSpy,
                        new AwaitingResponseGatewayDummy()).Execute("Session ID", "Message ID");

                    Assert.False(getCurrentNewGameSpy.ExecuteCalled);
                }
                
                [Test]
                public void ThenNewMessageIDIsCreatedAndAddedToAwaitingResponseGateway()
                {
                    GetStartingCardSpy getCurrentNewGameSpy = new GetStartingCardSpy();

                    new GameSessionNotFound(getCurrentNewGameSpy,
                        new AwaitingResponseGatewayDummy()).Execute("Session ID", "Message ID");

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
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(false);
                    Assert.Throws<InvalidSessionIDException>(() =>  new GameSessionNotFound(new GetStartingCardDummy(), spy).Execute(
                        invalidInput, "Message ID"));
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
                    IGetStartingCard getStartingCardDummy = new GetStartingCardDummy();

                    Assert.Throws<InvalidMessageIDException>(() =>  new GameSessionNotFound(getStartingCardDummy, spy).Execute(
                        "Session ID", invalidInput));
                }
            }
        }
    }
}