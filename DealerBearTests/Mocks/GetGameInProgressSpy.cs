using DealerBear.UseCases.GetGameInProgress.Interface;

namespace DealerBearTests.Mocks
{
    public class GetGameInProgressSpy : IGetGameInProgress
    {
        public bool ExecuteCalled { get; private set; }

        public void Execute(string sessionID)
        {
            ExecuteCalled = true;
        }
    }
}