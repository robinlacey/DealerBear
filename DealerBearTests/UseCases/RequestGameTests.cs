using System;
using DealerBear.Exceptions;
using DealerBear.Messages;
using DealerBear.UseCases.RequestGameData;
using DealerBear.UseCases.RequestGameData.Interface;
using DealerBearTests.Mocks;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class RequestGameTests
    {
        public class GivenInvalidInput
        {
            public class WhenSessionIDIsInvalid
            {
                [TestCase("   ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidSessionID(string invalidID)
                {
                    IRequestGameData requestGameData = new RequestGameData();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameData.Execute(new GameRequestStub(invalidID), new AwaitingResponseGatewayDummy(),
                            new PublishEndPointDummy()));
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
                requestGameData.Execute(new GameRequestStub(id), new AwaitingResponseGatewayDummy(), spy);
                IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse =
                    spy.MessageObject as IRequestGameIsSessionIDInUse;
                Assert.IsNotNull(requestGameIsSessionIDInUse);
                Assert.True(requestGameIsSessionIDInUse.SessionID == id);
            }

            [Test]
            public void ThenUIDIsAddedToRequestAndSavedToAwaitingResponseGateway()
            {
                AwaitingResponseGatewaySpy gatewaySpy = new AwaitingResponseGatewaySpy(false);
                PublishEndPointSpy publishSpy = new PublishEndPointSpy();
                IRequestGameData requestGameData = new RequestGameData();
                string id = Guid.NewGuid().ToString();
                requestGameData.Execute(new GameRequestStub(id), gatewaySpy, publishSpy);

                IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse =
                    publishSpy.MessageObject as IRequestGameIsSessionIDInUse;
                Assert.NotNull(requestGameIsSessionIDInUse);
                Assert.False(requestGameIsSessionIDInUse.MessageID == null ||
                             string.IsNullOrEmpty(requestGameIsSessionIDInUse.MessageID) ||
                             string.IsNullOrWhiteSpace(requestGameIsSessionIDInUse.MessageID));
                Assert.True(requestGameIsSessionIDInUse.MessageID == gatewaySpy.SaveIDInput);
            }
        }
    }
}