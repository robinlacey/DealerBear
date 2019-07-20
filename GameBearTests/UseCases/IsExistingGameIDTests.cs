using GameBear.Exceptions;
using GameBearTests.Mocks;
using GameBear.UseCases;
using GameBear.UseCases.IsExistingSession;
using GameBear.UseCases.IsExistingSession.Interface;
using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class IsExistingGameIDTests
    { 
        public class GivenInvalidInput
        {
            public class WhenSessionIDIsEmpty
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IIsExistingSession isExistingSession = new IsExistingSession();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        isExistingSession.Execute(new IsSessionIDInUseStub(string.Empty), new GameDataGatewayDummy(), new PublishEndPointDummy()));
                }
            }

            public class WhenSessionIDIsWhiteSpace
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IIsExistingSession isExistingSession = new IsExistingSession();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        isExistingSession.Execute(new IsSessionIDInUseStub("    "), new GameDataGatewayDummy(), new PublishEndPointDummy()));
                }
            }

            public class WhenSessionIDIsNull
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IIsExistingSession isExistingSession = new IsExistingSession();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        isExistingSession.Execute(new IsSessionIDInUseStub(null), new GameDataGatewayDummy(), new PublishEndPointDummy()));
                }
            }
        }

        public class GivenValidInput
        {
            public class WhenGameSessionGatewayIsCalled
            {
                [TestCase("Scout The Dog")]
                [TestCase("Is A Good Dog")]
                public void ThenGameSessionGatewayIsExistingSessionIsCalled(string sessionID)
                {
                    IIsExistingSession isExistingSession = new IsExistingSession();
                    GameDataGatewaySpy spy = new GameDataGatewaySpy();
                    isExistingSession.Execute(new IsSessionIDInUseStub(sessionID),spy,new PublishEndPointDummy());
                    Assert.True(spy.IsExistingSessionSessionID == sessionID);
                    
                }  
            }

            public class WhenGameSessionIsFound
            {
                [Test]
                public void ThenSessionIDIsPublishedToIsExistingSessionQueue()
                {
                    Assert.Fail();
                }
            }

            public class WhenGameSessionIsNotFound
            {
                [Test]
                public void ThenSessionIDIsPublishedToIsNewSessionQueue()
                {
                    Assert.Fail();
                }
            }
        }
    }
}