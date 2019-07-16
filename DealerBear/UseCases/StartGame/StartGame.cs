using DealerBear_API.Gateways.Interface;
using DealerBear_API.UseCases.Error;
using DealerBear_API.UseCases.StartGame.Interface;
using Newtonsoft.Json;

namespace DealerBear_API.UseCases.StartGame
{
    public class StartGame:IStartGame
    {
        private readonly IGameNamesGateway _gameNamesGateway;
        private readonly ISessionsGateway _sessionsGateway;

        public StartGame(IGameNamesGateway gameNamesGateway, ISessionsGateway sessionsGateway)
        {
            _gameNamesGateway = gameNamesGateway;
            _sessionsGateway = sessionsGateway;
        }
        public string Execute(string gameName, string sessionID)
        {
            StartGameError error = GetError(gameName, sessionID);
            if (error != null)
            {
                return JsonConvert.SerializeObject(error);
            }
            return "";
        }

        StartGameError GetError(string gameName, string sessionID)
        {
            if (string.IsNullOrWhiteSpace(sessionID) || string.IsNullOrEmpty(sessionID))
            {
                return new StartGameError("Invalid SessionID");
            }
            if (string.IsNullOrWhiteSpace(gameName) || string.IsNullOrEmpty(gameName))
            {
                return new StartGameError("Invalid GameName");
            }
            if (!_gameNamesGateway.IsValidGameName(gameName))
            {
                return new StartGameError("GameName not in use");
            }
            if (_sessionsGateway.IsActiveSession(sessionID))
            {
                return new StartGameError("SessionID already in use");
            }
            return null;
        }
    }
}