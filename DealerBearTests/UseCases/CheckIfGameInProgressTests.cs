using System;
using DealerBear.Exceptions;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.CheckIfGameInProgress;
using DealerBear.UseCases.CheckIfGameInProgress.Interface;
using DealerBearTests.Mocks;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class CheckIfGameInProgressTests
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
                    ICheckIfGameInProgress checkIfGameInProgress = new CheckIfGameInProgress(new AwaitingResponseGatewayDummy(),
                        new PublishEndPointDummy());
                    Assert.Throws<InvalidSessionIDException>(() =>
                        checkIfGameInProgress.Execute(invalidID));
                }
            }
        }

        public class GivenValidInput
        {
            [Test]
            public void ThenSessionIDIsPublishedToIsSessionIDInUseQueue()
            {
                PublishEndPointSpy spy = new PublishEndPointSpy();
                ICheckIfGameInProgress checkIfGameInProgress = new CheckIfGameInProgress(new AwaitingResponseGatewayDummy(), spy);
                string id = Guid.NewGuid().ToString();
                checkIfGameInProgress.Execute(id);
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
                ICheckIfGameInProgress checkIfGameInProgress = new CheckIfGameInProgress( gatewaySpy, publishSpy);
                string id = Guid.NewGuid().ToString();
                checkIfGameInProgress.Execute(id);

                IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse =
                    publishSpy.MessageObject as IRequestGameIsSessionIDInUse;
                Assert.NotNull(requestGameIsSessionIDInUse);
                Assert.False(requestGameIsSessionIDInUse.MessageID == null ||
                             string.IsNullOrEmpty(requestGameIsSessionIDInUse.MessageID) ||
                             string.IsNullOrWhiteSpace(requestGameIsSessionIDInUse.MessageID));
                Assert.True(requestGameIsSessionIDInUse.MessageID == gatewaySpy.SaveIDInput);
            }
            [Test]
            public void ThenNewMessageIDIsGUID()
            {
                PublishEndPointSpy spy = new PublishEndPointSpy();
                ICheckIfGameInProgress checkIfGameInProgress = new CheckIfGameInProgress(new AwaitingResponseGatewayDummy(), spy);
                string id = Guid.NewGuid().ToString();
                checkIfGameInProgress.Execute(id);
                IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse =
                    spy.MessageObject as IRequestGameIsSessionIDInUse;
                Assert.IsNotNull(requestGameIsSessionIDInUse);
                Assert.True(Guid.TryParse(requestGameIsSessionIDInUse.MessageID, out Guid _));
            }
        }
    }
}