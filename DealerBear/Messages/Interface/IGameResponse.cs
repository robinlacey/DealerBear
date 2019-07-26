using System;
using System.Collections.Generic;

namespace DealerBear.Messages.Interface
{
    public interface IGameResponse
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
        string CurrentCardID { get; set; }
        int Seed { get; set; }
        int PackVersion { get; set; }
        Dictionary<string,int> CurrentStats { get; set; }
        Tuple<string, float>[] CardsToAdd { get; set; }
    }
}