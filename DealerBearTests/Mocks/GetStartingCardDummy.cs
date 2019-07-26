using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using DealerBear.UseCases.GetStartingCard.Interface;

namespace DealerBearTests.Mocks
{
    public class GetStartingCardDummy : IGetStartingCard
    {
        public void Execute(string sessionID)
        {
            
        }
    }
}