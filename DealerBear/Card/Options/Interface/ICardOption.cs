using DealerBear.Player.Interface;

namespace DealerBear.Card.Options.Interface
{
    public interface ICardOption
    {
        string Title { get; }
        string Description { get; }
        IPlayerStats PlayerStats { get; }
    }
}