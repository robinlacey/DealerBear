using DealerBear.UseCases.GenerateSeed.Interface;

namespace DealerBearTests.Mocks
{
    public class GenerateSeedStub : IGenerateSeed
    {
        private readonly float _seedReturnValue;

        public GenerateSeedStub(float seedReturnValue)
        {
            _seedReturnValue = seedReturnValue;
        }

        public float Execute() => _seedReturnValue;
    }
}