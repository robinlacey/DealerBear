using DealerBear.Gateway.Interface;
using NUnit.Framework;

namespace DealerBearTests.Gateways
{
    public class InMemoryAwaitingResponseGateway
    {
        class GivenAnID
        {
            class WhenIDIsChecked
            {
                [TestCase("Scout")]
                [TestCase("The")]
                [TestCase("Dog")]
                public void ThenIDIsFound(string id)
                {
                    IAwaitingResponseGateway awaitingResponseGateway = new DealerBear.Gateway.InMemoryAwaitingResponseGateway();
                    awaitingResponseGateway.SaveID(id);
                    Assert.True(awaitingResponseGateway.HasID(id));
                }
            }
        }

        class GivenNoID
        {
            class WhenIDIsChecked
            {
                [TestCase("Scout")]
                [TestCase("The")]
                [TestCase("Dog")]
                public void ThenIDIsNotFound(string id)
                {
                    IAwaitingResponseGateway awaitingResponseGateway = new DealerBear.Gateway.InMemoryAwaitingResponseGateway();
                    Assert.False(awaitingResponseGateway.HasID(id));
                }
            }

            class WhenIDIsPopped
            {
                [TestCase("Scout")]
                [TestCase("The")]
                [TestCase("Dog")]
                public void ThenIDIsNotFound(string id)
                {
                    IAwaitingResponseGateway awaitingResponseGateway = new DealerBear.Gateway.InMemoryAwaitingResponseGateway();
                    awaitingResponseGateway.SaveID(id);
                    Assert.True(awaitingResponseGateway.HasID(id));
                    awaitingResponseGateway.PopID(id);
                    Assert.False(awaitingResponseGateway.HasID(id));
                }
            }
        }
    }
}