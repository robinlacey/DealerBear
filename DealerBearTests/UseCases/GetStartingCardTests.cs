using System;
using DealerBear.Exceptions;
using DealerBear.Messages.Interface;
using DealerBear.UseCases.GetStartingCard;
using DealerBearTests.Mocks;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class GetStartingCardTests
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
                    Assert.Throws<InvalidSessionIDException>(() => new GetStartingCard( new PackVersionGatewayDummy(),new AwaitingResponseGatewayDummy(),  new GenerateSeedDummy(), publishEndPointSpy).Execute(sessionID));
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
                    new GetStartingCard(new PackVersionGatewayDummy(), new AwaitingResponseGatewayDummy(), new GenerateSeedDummy(),
                        publishEndPointSpy).Execute(sessionID );
                    Assert.True(publishEndPointSpy.MessageObject is IRequestStartingCard);
                    IRequestStartingCard newGameData = (IRequestStartingCard) publishEndPointSpy.MessageObject;
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
                    PackVersionGatewayStub stub = new PackVersionGatewayStub(version);
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    new GetStartingCard( stub,new AwaitingResponseGatewayDummy(),  new GenerateSeedDummy(),  publishEndPointSpy).Execute("SessionID");
                    Assert.True(publishEndPointSpy.MessageObject is IRequestStartingCard);
                    IRequestStartingCard newGameData = (IRequestStartingCard) publishEndPointSpy.MessageObject;
                    Assert.True(newGameData.PackVersionNumber == version);
                }
            }

            public class WhenGenerateSeedUseCaseIsCalled
            {
                [TestCase(12351241)]
                [TestCase(532452345)]
                [TestCase(-234)]
                public void ThenValueIsSavedToCreateGameGateway(int seedGeneratorReturnValue)
                {
                    GenerateSeedStub stub = new GenerateSeedStub(seedGeneratorReturnValue);
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    new GetStartingCard( new PackVersionGatewayDummy(),new AwaitingResponseGatewayDummy(),  stub, publishEndPointSpy).Execute("SessionID");
                    Assert.True(publishEndPointSpy.MessageObject is IRequestStartingCard);
                    IRequestStartingCard newGameData = (IRequestStartingCard) publishEndPointSpy.MessageObject;
                    Assert.True(Math.Abs(newGameData.Seed - seedGeneratorReturnValue) < 0.1f);
                }
            }
            
            //Idempotent
            public class WhenMessageIDIsGenerated
            {
                [Test]
                public void ThenNewMessageIDIsGUID()
                {
                    GenerateSeedStub stub = new GenerateSeedStub(0);
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    new GetStartingCard(new PackVersionGatewayDummy(),new AwaitingResponseGatewayDummy(), stub, publishEndPointSpy).Execute("SessionID");
                    Assert.True(publishEndPointSpy.MessageObject is IRequestStartingCard);
                    IRequestStartingCard newGameData = (IRequestStartingCard) publishEndPointSpy.MessageObject;
                    Assert.True(Guid.TryParse(newGameData.MessageID, out Guid _));
                }

                [Test]
                public void ThenNewMessageIdIsAddedToAwaitingResponseGateway()
                {
                    GenerateSeedStub stub = new GenerateSeedStub(0);
                    PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                    AwaitingResponseGatewaySpy spy = new AwaitingResponseGatewaySpy(true);
                    new GetStartingCard(new PackVersionGatewayDummy(),spy, stub, publishEndPointSpy).Execute("SessionID");
                    Assert.True(publishEndPointSpy.MessageObject is IRequestStartingCard);
                    IRequestStartingCard newGameData = (IRequestStartingCard) publishEndPointSpy.MessageObject;
                    Assert.True(spy.SaveIDInput == newGameData.MessageID);
                }
            }
        }
    }
}