using GameBear.Exceptions;
using GameBearTests.Mocks;
using GameBear.UseCases.RequestGameCheckExistingSession;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
using Messages;
using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class RequestGameCheckExistingSessionTests
    {
        public class GivenInvalidInput
        {
            public class WhenSessionIDIsEmpty
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IRequestGameCheckExistingSession requestGameCheckExistingSession = new RequestGameCheckExistingSession();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameCheckExistingSession.Execute(new RequestGameIsSessionIDInUseStub(string.Empty),
                            new GameDataGatewayDummy(), new PublishEndPointDummy()));
                }
            }

            public class WhenSessionIDIsWhiteSpace
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IRequestGameCheckExistingSession requestGameCheckExistingSession = new RequestGameCheckExistingSession();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameCheckExistingSession.Execute(new RequestGameIsSessionIDInUseStub("    "), new GameDataGatewayDummy(),
                            new PublishEndPointDummy()));
                }
            }

            public class WhenSessionIDIsNull
            {
                [Test]
                public void ThenThrowsInvalidSessionID()
                {
                    IRequestGameCheckExistingSession requestGameCheckExistingSession = new RequestGameCheckExistingSession();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameCheckExistingSession.Execute(new RequestGameIsSessionIDInUseStub(null), new GameDataGatewayDummy(),
                            new PublishEndPointDummy()));
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
                    IRequestGameCheckExistingSession requestGameCheckExistingSession = new RequestGameCheckExistingSession();
                    GameDataGatewaySpy spy = new GameDataGatewaySpy();
                    requestGameCheckExistingSession.Execute(new RequestGameIsSessionIDInUseStub(sessionID), spy,
                        new PublishEndPointDummy());
                    Assert.True(spy.IsExistingSessionSessionID == sessionID);
                }
            }

            public class WhenGameSessionIsFound
            {
                [Test]
                public void ThenSessionIDIsPublishedToSessionFoundQueue()
                {
                    IRequestGameCheckExistingSession requestGameCheckExistingSession = new RequestGameCheckExistingSession();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, true);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    requestGameCheckExistingSession.Execute(new RequestGameIsSessionIDInUseStub("Scout Is A Good Dog"), stub, spy);
                    Assert.True(spy.MessageObject is IRequestGameSessionFound);
                }
            }

            public class WhenGameSessionIsNotFound
            {
                [Test]
                public void ThenSessionIDIsPublishedToSessionNotFoundQueue()
                {
                    IRequestGameCheckExistingSession requestGameCheckExistingSession = new RequestGameCheckExistingSession();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    requestGameCheckExistingSession.Execute(new RequestGameIsSessionIDInUseStub("Scout Is A Good Dog"), stub, spy);
                    Assert.True(spy.MessageObject is IRequestGameSessionNotFound);
                }
            }
        }
    }
}