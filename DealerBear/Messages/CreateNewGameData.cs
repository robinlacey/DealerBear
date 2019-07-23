namespace DealerBear.Messages
{
    public class CreateNewGameData : ICreateNewGameData
    {
        public string SessionID { get; set; }
        public float Seed { get; set; }
        public int PackVersionNumber { get; set; }
    }
}