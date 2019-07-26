using DealerBear.Adaptor.Interface;
using DealerBear.Gateway.Interface;
using DealerBear.UseCases.GenerateSeed.Interface;
using DealerBear.UseCases.GetStartingCard.Interface;

namespace DealerBearTests.Mocks
{
    public class GetStartingCardSpy : IGetStartingCard
    {
        public bool ExecuteCalled { get; private set; }


        public void Execute(string sessionID)
        {
            ExecuteCalled = true;
        }
    }
}