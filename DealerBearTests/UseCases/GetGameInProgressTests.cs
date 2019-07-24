using System;
using DealerBear.Exceptions;
using DealerBear.Messages;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.GetGameInProgress;
using DealerBear.UseCases.GetGameInProgress.Interface;
using DealerBearTests.Mocks;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class GetGameInProgressTests
    {

        public class GivenInvalidInput
        {
            public class WhenSessionIDIsInvalid
            {
                [TestCase("  ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidSessionID(string sessionID)
                {
                    IGetGameInProgress getGameInProgress = new GetGameInProgress();
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    Assert.Throws<InvalidSessionIDException>(() => getGameInProgress.Execute(sessionID,new AwaitingResponseGatewayDummy(),publishEndPointSpy));
                }
            }
        }

        public class GivenValidInput
        {
            public class WhenSessionIDIsPassedIn
            {
                [TestCase("Scout The Dog")]
                [TestCase("Is A Good Dog")]
                public void ThenValueIsSavedToCreateGameGateway(string sessionID)
                {
                    IGetGameInProgress createNewGame = new GetGameInProgress();
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    createNewGame.Execute(sessionID, new AwaitingResponseGatewayDummy(), publishEndPointSpy);
                    Assert.True(publishEndPointSpy.MessageObject is IGetGameData);
                    IGetGameData newGameData = (IGetGameData) publishEndPointSpy.MessageObject;
                    Assert.True(newGameData.SessionID == sessionID);
                }
            }
            public class WhenMessageIDIsGenerated
            {
                [Test]
                public void ThenNewMessageIDIsGUID()
                {
                    IGetGameInProgress getGameInProgress = new GetGameInProgress();
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    getGameInProgress.Execute("SessionID", new AwaitingResponseGatewayDummy(), publishEndPointSpy);
                    Assert.True(publishEndPointSpy.MessageObject is IGetGameData);
                    IGetGameData gameData = (IGetGameData) publishEndPointSpy.MessageObject;
                    Assert.True(Guid.TryParse(gameData.MessageID, out Guid _));
                }

                [Test]
                public void ThenNewMessageIdIsAddedToAwaitingResponseGateway()
                {
                    IGetGameInProgress getGameInProgress = new GetGameInProgress();
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(true);
                    getGameInProgress.Execute("SessionID",spy, publishEndPointSpy);
                    Assert.True(publishEndPointSpy.MessageObject is IGetGameData);
                    IGetGameData newGameData = (IGetGameData) publishEndPointSpy.MessageObject;
                    Assert.True(spy.SaveIDInput == newGameData.MessageID);
                }
            }
        }
    }
}