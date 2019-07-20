using DealerBear.UseCases.CreateGameState.Interface;

namespace DealerBearTests.Mocks
{
    public class CreateGameStateSpy:ICreateGameState
    {
        public bool ExecuteCalled { get; private set; }

        public void Execute()
        {
            ExecuteCalled = true;
        }
    }
}