using DealerBear.Card.Options.Interface;

namespace DealerBear.Card.Interface
{
    public interface ICard
    {
        string CardID { get; }
        string Title { get; }
        string Description { get; }
        string ImageURL { get; }
        ICardOption[] Options { get; set; }
        
    }
}