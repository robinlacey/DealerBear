namespace DealerBear.Player.Interface
{
    public interface IStat
    {
         int Current { get; }
         int Minimum { get; }
         int Maximum { get; }
    }
}