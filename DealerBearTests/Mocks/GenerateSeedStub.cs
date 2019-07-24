using DealerBear.UseCases.GenerateSeed.Interface;

namespace DealerBearTests.Mocks
{
    public class GenerateSeedStub : IGenerateSeed
    {
        private readonly int _seedReturnValue;

        public GenerateSeedStub(int seedReturnValue)
        {
            _seedReturnValue = seedReturnValue;
        }

        public int Execute() => _seedReturnValue;
    }
}