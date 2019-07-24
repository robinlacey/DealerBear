using DealerBear.Card.Interface;

namespace DealerBear.Card.Add.Interface
{
    public interface IAddCard
    {
        ICard Card { get;  }
        float Probability { get; }
    }
}