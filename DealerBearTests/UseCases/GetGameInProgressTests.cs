using System;
using DealerBear.Exceptions;
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
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    IGetGameInProgress getGameInProgress = new GetGameInProgress(new AwaitingResponseGatewayDummy(),publishEndPointSpy);
                    Assert.Throws<InvalidSessionIDException>(() => getGameInProgress.Execute(sessionID));
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
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    IGetGameInProgress createNewGame = new GetGameInProgress(new AwaitingResponseGatewayDummy(), publishEndPointSpy);
                  
                    createNewGame.Execute(sessionID);
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
              
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    IGetGameInProgress getGameInProgress = new GetGameInProgress( new AwaitingResponseGatewayDummy(), publishEndPointSpy);
                    getGameInProgress.Execute("SessionID");
                    Assert.True(publishEndPointSpy.MessageObject is IGetGameData);
                    IGetGameData gameData = (IGetGameData) publishEndPointSpy.MessageObject;
                    Assert.True(Guid.TryParse(gameData.MessageID, out Guid _));
                }

                [Test]
                public void ThenNewMessageIdIsAddedToAwaitingResponseGateway()
                {
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(true);
                    
                    IGetGameInProgress getGameInProgress = new GetGameInProgress(spy, publishEndPointSpy); 
                    getGameInProgress.Execute("SessionID");
                    Assert.True(publishEndPointSpy.MessageObject is IGetGameData);
                    IGetGameData newGameData = (IGetGameData) publishEndPointSpy.MessageObject;
                    Assert.True(spy.SaveIDInput == newGameData.MessageID);
                }
            }
        }
    }
}