namespace DealerBear.UseCases.CheckIfGameInProgress.Interface
{
    public interface ICheckIfGameInProgress
    {
        void Execute(string sessionID);
    }
}