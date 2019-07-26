using DealerBear.Card.Interface;
using DealerBear.Card.Options.Interface;

namespace DealerBearTests.Mocks
{
    public class CardDummy : ICard
    {
        public string CardID { get; }
        public string Title { get; }
        public string Description { get; }
        public string ImageURL { get; }
        public ICardOption[] Options { get; set; }
    }
}