using System;
using DealerBear.Exceptions;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.CreateNewGame;
using DealerBear.UseCases.CreateNewGame.Interface;
using DealerBearTests.Mocks;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class CreateNewGameTests
    {
        public class GivenInvalidInput
        {
            public class GivenNoMatchingMessageID
            {
                [Test]
                public void ThenNoMessagesArePublished()
                {
                    AwaitingResponseGatewaySpy stub = new AwaitingResponseGatewaySpy(false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    ICreateNewGame createNewGame = new CreateNewGame();
                    createNewGame.Execute("SessionID", "MessageID", "StartingCardID",10,1, stub,spy);
                    Assert.False(spy.PublishRun);
                }
            }

            public class GivenInvalidSessionID
            {
                [TestCase("")]
                [TestCase("   ")]
                [TestCase(null)]
                public void ThenThrowsInvalidSessionIDException(string invalidSessionID)
                {
                    AwaitingResponseGatewaySpy stub = new AwaitingResponseGatewaySpy(false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    ICreateNewGame createNewGame = new CreateNewGame();
                    Assert.Throws<InvalidSessionIDException>(()=>createNewGame.Execute(invalidSessionID, "MessageID", "StartingCardID",10,1, stub,spy));
                }
            }
            public class GivenInvalidMessageID
            {
                [TestCase("")]
                [TestCase("   ")]
                [TestCase(null)]
                public void ThenThrowsInvalidMessageIDException(string invalidMessageID)
                {
                    AwaitingResponseGatewaySpy stub = new AwaitingResponseGatewaySpy(false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    ICreateNewGame createNewGame = new CreateNewGame();
                    Assert.Throws<InvalidMessageIDException>(()=>createNewGame.Execute("SessionID", invalidMessageID, "StartingCardID",10,1, stub,spy));
                }
            }
        }

        public class GivenValidInput
        {
            [TestCase("Scout The Messenger Dog")]
            [TestCase("Is Digging Holes In The Garden")]
            public void ThenNewMessageIDIsCheckedAddedAndPublished(string messageID)
            {
                AwaitingResponseGatewaySpy awaitingResponseGatewaySpy = new AwaitingResponseGatewaySpy(true);
                PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                ICreateNewGame createNewGame = new CreateNewGame();
                createNewGame.Execute("SessionID", messageID, "StartingCardID",10,1, awaitingResponseGatewaySpy,publishEndPointSpy);
                AssertMessageIDIsCheckedAndRemoved(messageID, awaitingResponseGatewaySpy);
                Assert.True(publishEndPointSpy.MessageObject is ICreateNewGameRequest);
                AssertNewMessageIDIsGeneratedAndAddedToGateway(messageID, publishEndPointSpy, awaitingResponseGatewaySpy);
            }

            private static void AssertNewMessageIDIsGeneratedAndAddedToGateway(string messageID,
                PublishEndPointSpy publishEndPointSpy, AwaitingResponseGatewaySpy awaitingResponseGatewaySpy)
            {
                ICreateNewGameRequest message = (ICreateNewGameRequest) publishEndPointSpy.MessageObject;
                Assert.True(awaitingResponseGatewaySpy.SaveIDInput == message.MessageID);
                Assert.True(message.MessageID != messageID);
                Assert.True(Guid.TryParse(message.MessageID, out Guid _));
            }

            private static void AssertMessageIDIsCheckedAndRemoved(string messageID,
                AwaitingResponseGatewaySpy awaitingResponseGatewaySpy)
            {
                Assert.True(awaitingResponseGatewaySpy.HasIDInput == messageID);
                Assert.True(awaitingResponseGatewaySpy.PopIDInput == messageID);
            }
            [TestCase("Scout The Card Dog")]
            [TestCase("Is Digging Holes In The Garden")]
            public void ThenCardIDIsPublished(string cardID)
            {
                AwaitingResponseGatewaySpy awaitingResponseGatewaySpy = new AwaitingResponseGatewaySpy(true);
                PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                ICreateNewGame createNewGame = new CreateNewGame();
                createNewGame.Execute("SessionID", "MessageID", cardID,10,1, awaitingResponseGatewaySpy,publishEndPointSpy);
                Assert.True(publishEndPointSpy.MessageObject is ICreateNewGameRequest);
                ICreateNewGameRequest message = (ICreateNewGameRequest) publishEndPointSpy.MessageObject;
                Assert.True(message.StartingCardID == cardID);
            }
            [TestCase(10)]
            [TestCase(231)]
            [TestCase(-100)]
            public void ThenSeedIsPublished(int seed)
            {
                AwaitingResponseGatewaySpy awaitingResponseGatewaySpy = new AwaitingResponseGatewaySpy(true);
                PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                ICreateNewGame createNewGame = new CreateNewGame();
                createNewGame.Execute("SessionID", "MessageID", "CardID",seed,1, awaitingResponseGatewaySpy,publishEndPointSpy);
                Assert.True(publishEndPointSpy.MessageObject is ICreateNewGameRequest);
                ICreateNewGameRequest message = (ICreateNewGameRequest) publishEndPointSpy.MessageObject;
                Assert.True(message.Seed == seed);
            }
            [TestCase(120)]
            [TestCase(2431)]
            [TestCase(-1020)]
            public void ThenPackVersionIsPublished(int packNumber)
            {
                AwaitingResponseGatewaySpy awaitingResponseGatewaySpy = new AwaitingResponseGatewaySpy(true);
                PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                ICreateNewGame createNewGame = new CreateNewGame();
                createNewGame.Execute("SessionID", "MessageID", "CardID",1,packNumber, awaitingResponseGatewaySpy,publishEndPointSpy);
                Assert.True(publishEndPointSpy.MessageObject is ICreateNewGameRequest);
                ICreateNewGameRequest message = (ICreateNewGameRequest) publishEndPointSpy.MessageObject;
                Assert.True(message.PackVersionNumber == packNumber);
            }
            [TestCase("Scout")]
            [TestCase("Is A Hot Dog")]
            public void ThenSessionIDIsPublished(string sessionID)
            {
                AwaitingResponseGatewaySpy awaitingResponseGatewaySpy = new AwaitingResponseGatewaySpy(true);
                PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                ICreateNewGame createNewGame = new CreateNewGame();
                createNewGame.Execute(sessionID, "MessageID", "CardID",10,1, awaitingResponseGatewaySpy,publishEndPointSpy);
                Assert.True(publishEndPointSpy.MessageObject is ICreateNewGameRequest);
                ICreateNewGameRequest message = (ICreateNewGameRequest) publishEndPointSpy.MessageObject;
                Assert.True(message.SessionID == sessionID);
            }
        }
    }
}