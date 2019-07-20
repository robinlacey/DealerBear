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
                        requestGameData.Execute(new GameRequestStub(string.Empty), new AwaitingResponseGatewayDummy(), new PublishEndPointDummy()));
                }
            }

            public class WhenSessionIDIsWhiteSpace
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IRequestGameData requestGameData = new RequestGameData();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameData.Execute(new GameRequestStub("    "), new AwaitingResponseGatewayDummy(), new PublishEndPointDummy()));
                }
            }

            public class WhenSessionIDIsNull
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IRequestGameData requestGameData = new RequestGameData();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameData.Execute(new GameRequestStub(null), new AwaitingResponseGatewayDummy(),  new PublishEndPointDummy()));
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
                IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse = spy.MessageObject as IRequestGameIsSessionIDInUse;
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
                
                IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse = publishSpy.MessageObject as IRequestGameIsSessionIDInUse;
                Assert.NotNull(requestGameIsSessionIDInUse);
                
                string transactionUID = requestGameIsSessionIDInUse.MessageID;
                Assert.False(string.IsNullOrEmpty(transactionUID) || string.IsNullOrWhiteSpace(transactionUID) || transactionUID == null);
                Assert.True(requestGameIsSessionIDInUse.MessageID == id);
                Assert.True(gatewaySpy.HasUIDInput == transactionUID);
                
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