namespace DealerBear_API.UseCases.Error
{
    public class StartGameError:UseCaseError
    {
        public StartGameError(string message)
        {
            Message = message;
        }

        public sealed override string Message { get; set; }
    }
}