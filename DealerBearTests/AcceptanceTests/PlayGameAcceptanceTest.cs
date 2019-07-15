namespace DealerBearTests.AcceptanceTests
{
    public class PlayGameAcceptanceTest
    {
        // StartGame (ChatID) Assert new GameID and Card1
        // GetCurrentGame (ChatID, GameID) Assert Card is Card1 - state hasnt changed
        // PlayCard (ChatID, GameID, OptionA) Assert Card2 and Stats have changed
        // GetCurrentGame (ChatID, GameID)  Assert Card is Card2  
        // PlayCard (ChatID, GameID, OptionA) Assert no card. Game Over state
        // GetCurrentGame (ChatID, GameID) Assert Game Over state
        // StartGame (ChatID) Assert new GameID and CardA
    }
}