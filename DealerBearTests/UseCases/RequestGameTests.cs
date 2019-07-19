using System;
using DealerBear.Exceptions;
using DealerBear.UseCases.RequestGameData;
using DealerBear.UseCases.RequestGameData.Interface;
using DealerBearTests.Mocks;
using Messages;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class RequestGameTests
    {
        public class GivenInvalidInput
        {
            public class WhenSessionIDIsEmpty
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IRequestGameData requestGameData = new RequestGameData();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameData.Execute(new GameRequestStub(string.Empty), new PublishEndPointDummy()));
                }
            }

            public class WhenSessionIDIsWhiteSpace
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IRequestGameData requestGameData = new RequestGameData();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameData.Execute(new GameRequestStub("    "), new PublishEndPointDummy()));
                }
            }

            public class WhenSessionIDIsNull
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IRequestGameData requestGameData = new RequestGameData();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameData.Execute(new GameRequestStub(null), new PublishEndPointDummy()));
                }
            }
        }

        public class GivenValidInput
        {
            [Test]
            public void ThenSessionIDIsPublishedToIsSessionIDInUseQueue()
            {
                PublishEndPointSpy spy = new PublishEndPointSpy();
                IRequestGameData requestGameData = new RequestGameData();
                string id = Guid.NewGuid().ToString();
                requestGameData.Execute(new GameRequestStub(id), spy);
                IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse = spy.MessageObject as IRequestGameIsSessionIDInUse;
                Assert.IsNotNull(requestGameIsSessionIDInUse);
                Assert.True(requestGameIsSessionIDInUse.SessionID == id);
            }
        }

        public class IdopotentTests
        {
            [Test]
            public void TODO()
            {
                Assert.Fail(
                    "Note: Potential slip ups: " +
                    "1 - Starting a new game (dont want to start multiple games - check if session exists on game create game" +
                    "2 - (not this use case) when playing a game pass in CardID )");
            }
        }
    }
}