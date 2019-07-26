using System.Collections.Generic;

namespace DealerBear.Card.Options.Interface
{
    public interface ICardOption
    {
        string Title { get; }
        string Description { get; }
        Dictionary<string,int> PlayerStats { get; }
    }
}