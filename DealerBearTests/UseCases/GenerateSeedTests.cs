using DealerBear.UseCases.GenerateSeed;
using DealerBear.UseCases.GenerateSeed.Interface;
using NUnit.Framework;

namespace DealerBearTests.UseCases
{
    public class GenerateSeedTests
    {
        [TestCase(10)]
        [TestCase(14)]
        [TestCase(99)]
        public void DoesNotReturnTheSameValues(int attempts)
        {
            IGenerateSeed generateSeed = new GenerateSeed();
            int lastSeed = 0;
            for (int i = 0; i < attempts; i++)
            {
                int seed = generateSeed.Execute();
                Assert.True(lastSeed != seed);
                lastSeed = seed;
            }
        }
    }
}