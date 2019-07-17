using System;
using DealerBear_API.Exceptions;
using DealerBear_API.UseCases.RequestGameData;
using DealerBear_API.UseCases.StartGame.Interface;
using DealerBearTests.Mocks;
using MassTransit;
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
                IIsSessionIDInUse isSessionIDInUse = spy.MessageObject as IIsSessionIDInUse;
                Assert.IsNotNull(isSessionIDInUse);
                Assert.True(isSessionIDInUse.SessionID == id);
            }
        }
    }
}