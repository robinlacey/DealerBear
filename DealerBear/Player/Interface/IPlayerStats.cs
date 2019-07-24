using System.Collections.Generic;

namespace DealerBear.Player.Interface
{
    public interface IPlayerStats
    {
        Dictionary<string,IStat> Stats { get; set; }
    }
}