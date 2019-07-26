using DealerBear.Gateway;
using NUnit.Framework;

namespace DealerBearTests.Gateways
{
    public class InMemoryPackVersionGatewayTests
    {
        public class GivenNoPackVersion
        {
            public class WhenGetCurrentPackVersionCalled
            {
                [Test]
                public void WillStartAtZero()
                {
                    Assert.True(new InMemoryPackVersionGateway().GetCurrentPackVersion() == 0);
                }
            }
        }
        public class GivenNewPackVersion
        {
            public class WhenGetCurrentPackVersionCalled
            {
                [TestCase(99)]
                [TestCase(-10)]
                [TestCase(123)]
                public void WillStartAtZero(int newPackVersion)
                {
                   InMemoryPackVersionGateway packGateway = new InMemoryPackVersionGateway();
                   Assert.True(packGateway.GetCurrentPackVersion() == 0);
                   packGateway.SetCurrentPackVersion(newPackVersion);
                   Assert.True(packGateway.GetCurrentPackVersion() == newPackVersion);
                }
            }
            
            public class WhenGetCurrentPackVersionCalledMultipleTimes
            {
                [TestCase(4)]
                [TestCase(5)]
                [TestCase(12)]
                public void WillStartAtZero(int count)
                {
                    InMemoryPackVersionGateway packGateway = new InMemoryPackVersionGateway();
                    for (int i = 0; i < count; i++)
                    {
                        packGateway.SetCurrentPackVersion(i);
                        Assert.True(packGateway.GetCurrentPackVersion() == i);
                    }
                }
            }
        }
    }
}