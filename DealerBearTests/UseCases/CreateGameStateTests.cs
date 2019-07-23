using System;
using DealerBear.Exceptions;
using DealerBear.Messages;
using DealerBear.UseCases.CreateGameState;
using DealerBear.UseCases.CreateGameState.Interface;
using DealerBearTests.Mocks;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class CreateGameStateTests
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
                    ICreateGameState createGameState = new CreateGameState();
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    Assert.Throws<InvalidSessionIDException>(() => createGameState.Execute(sessionID,
                        new PackVersionGatewayDummy(), new GenerateSeedDummy(), publishEndPointSpy));
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
                    ICreateGameState createGameState = new CreateGameState();
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    createGameState.Execute(sessionID, new PackVersionGatewayDummy(), new GenerateSeedDummy(),
                        publishEndPointSpy);
                    Assert.True(publishEndPointSpy.MessageObject is ICreateNewGameData);
                    ICreateNewGameData newGameData = (ICreateNewGameData) publishEndPointSpy.MessageObject;
                    Assert.True(newGameData.SessionID == sessionID);
                }
            }

            public class WhenPackVersionGatewayIsCalled
            {
                [TestCase(1)]
                [TestCase(199)]
                [TestCase(-10)]
                public void ThenValueIsSavedToCreateGameGateway(int version)
                {
                    ICreateGameState createGameState = new CreateGameState();
                    PackVersionGatewayStub stub = new PackVersionGatewayStub(version);
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    createGameState.Execute("SessionID", stub, new GenerateSeedDummy(), publishEndPointSpy);
                    Assert.True(publishEndPointSpy.MessageObject is ICreateNewGameData);
                    ICreateNewGameData newGameData = (ICreateNewGameData) publishEndPointSpy.MessageObject;
                    Assert.True(newGameData.PackVersionNumber == version);
                }
            }

            public class WhenGenerateSeedUseCaseIsCalled
            {
                [TestCase(12351241)]
                [TestCase(1.1231415f)]
                [TestCase(-0.1276767f)]
                public void ThenValueIsSavedToCreateGameGateway(float seedGeneratorReturnValue)
                {
                    ICreateGameState createGameState = new CreateGameState();
                    GenerateSeedStub stub = new GenerateSeedStub(seedGeneratorReturnValue);
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    createGameState.Execute("SessionID", new PackVersionGatewayDummy(), stub, publishEndPointSpy);
                    Assert.True(publishEndPointSpy.MessageObject is ICreateNewGameData);
                    ICreateNewGameData newGameData = (ICreateNewGameData) publishEndPointSpy.MessageObject;
                    Assert.True(Math.Abs(newGameData.Seed - seedGeneratorReturnValue) < 0.1f);
                }
            }
        }
    }
}