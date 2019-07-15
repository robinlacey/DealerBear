using DealerBear_API.UseCases.Error;
using DealerBear_API.UseCases.StartGame;
using DealerBear_API.UseCases.StartGame.Interface;
using DealerBearTests.Mocks.Gateways;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class StartGameTests
    {
        public class GivenInvalidInput
        {
            public class WhenSessionIDIsEmpty
            {
                [Test]
                public void ThenReturnDataContainsError()
                {
                    IStartGame startGame = new StartGame(new GameNamesGatewayDummy(), new SessionsGatewayDummy());
                    
                    string returnData = startGame.Execute("NewGame", string.Empty);
                    StartGameError startGameError = JsonConvert.DeserializeObject<StartGameError>(returnData);
                    
                    Assert.IsNotNull(startGameError);
                    Assert.True(startGameError.Message.ToLower().Contains("sessionid"));
                    Assert.True(startGameError.Message.ToLower().Contains("invalid"));
                }
            }
            
            public class WhenSessionIDIsWhiteSpace
            {
                [Test]
                public void ThenReturnDataContainsError()
                {
                    IStartGame startGame = new StartGame(new GameNamesGatewayDummy(),new SessionsGatewayDummy());
                    
                    string returnData = startGame.Execute("NewGame", "   ");
                    StartGameError startGameError = JsonConvert.DeserializeObject<StartGameError>(returnData);
                    
                    Assert.IsNotNull(startGameError);
                    Assert.True(startGameError.Message.ToLower().Contains("sessionid"));
                    Assert.True(startGameError.Message.ToLower().Contains("invalid"));
                }
            }
            
            public class WhenSessionIDIsNull
            {
                [Test]
                public void ThenReturnDataContainsError()
                {
                    IStartGame startGame = new StartGame(new GameNamesGatewayDummy(),new SessionsGatewayDummy());
                    
                    string returnData = startGame.Execute("NewGame", null);
                    StartGameError startGameError = JsonConvert.DeserializeObject<StartGameError>(returnData);
                    
                    Assert.IsNotNull(startGameError);
                    Assert.True(startGameError.Message.ToLower().Contains("sessionid"));
                    Assert.True(startGameError.Message.ToLower().Contains("invalid"));
                }
            }
            
            public class WhenGameNameIsEmpty
            {
                [Test]
                public void ThenReturnDataContainsError()
                {
                    IStartGame startGame = new StartGame(new GameNamesGatewayDummy(), new SessionsGatewayDummy());
                    
                    string returnData = startGame.Execute("", "SessionID");
                    StartGameError startGameError = JsonConvert.DeserializeObject<StartGameError>(returnData);
                    
                    Assert.IsNotNull(startGameError);
                    Assert.True(startGameError.Message.ToLower().Contains("gamename"));
                    Assert.True(startGameError.Message.ToLower().Contains("invalid"));
                }
            }
            
            public class WhenGameNameIsWhiteSpace
            {
                [Test]
                public void ThenReturnDataContainsError()
                {
                    IStartGame startGame = new StartGame(new GameNamesGatewayDummy(),new SessionsGatewayDummy());
                    
                    string returnData = startGame.Execute("   ", "SessionID");
                    StartGameError startGameError = JsonConvert.DeserializeObject<StartGameError>(returnData);
                   
                    Assert.IsNotNull(startGameError);
                    Assert.True(startGameError.Message.ToLower().Contains("gamename"));
                    Assert.True(startGameError.Message.ToLower().Contains("invalid"));
                }
            }
            
            public class WhenGameNameIsNull
            {
                [Test]
                public void ThenReturnDataContainsError()
                {
                    IStartGame startGame = new StartGame(new GameNamesGatewayDummy(),new SessionsGatewayDummy());
                    
                    string returnData = startGame.Execute(null, "SessionID");
                    StartGameError startGameError = JsonConvert.DeserializeObject<StartGameError>(returnData);
                    
                    Assert.IsNotNull(startGameError);
                    Assert.True(startGameError.Message.ToLower().Contains("gamename"));
                    Assert.True(startGameError.Message.ToLower().Contains("invalid"));
                }
            }

            
            public class WhenGameNamesGatewayCanNotFindName
            {
                [Test]
                public void ThenReturnDataContainsError()
                {
                    IStartGame startGame = new StartGame(new GameNameGatewayStub(false),new SessionsGatewayDummy());
                    
                    string returnData = startGame.Execute("NewGame", "SessionID");
                    StartGameError startGameError = JsonConvert.DeserializeObject<StartGameError>(returnData);
                    
                    Assert.IsNotNull(startGameError);
                    Assert.True(startGameError.Message.ToLower().Contains("gamename"));
                    Assert.True(startGameError.Message.ToLower().Contains("not in use"));
                }
            }

            public class WhenSessionIDIsInUse
            {
                public class ThenReturnCurrentGameData
                {
                    [Test]
                    public void ThenReturnDataContainsError()
                    {
                        IStartGame startGame = new StartGame(new GameNameGatewayStub(true),new SessionsGatewayStub(true));
                        
                        string returnData = startGame.Execute("NewGame", "SessionID");
                        StartGameError startGameError = JsonConvert.DeserializeObject<StartGameError>(returnData);
                        
                        Assert.IsNotNull(startGameError);
                        Assert.True(startGameError.Message.ToLower().Contains("sessionid"));
                        Assert.True(startGameError.Message.ToLower().Contains("in use"));
                        Assert.False(startGameError.Message.ToLower().Contains("not in use"));
                    }
                }
            }
        }
    }
}