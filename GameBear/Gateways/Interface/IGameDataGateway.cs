using System.Globalization;
using Messages;

namespace GameBear.Gateways.Interface
{
    public interface IGameDataGateway
    {
        IGameData GetGameData(string sessionID);
        bool IsExistingSession(string sessionID);
    }
}