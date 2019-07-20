using DealerBear.UseCases.GetCurrentGameState.Interface;

namespace DealerBearTests.Mocks
{
    public class GetCurrentGameStateSpy : IGetCurrentGameState
    {
        public bool ExecuteCalled { get; private set; }

        public void Execute()
        {
            ExecuteCalled = true;
        }
    }
}