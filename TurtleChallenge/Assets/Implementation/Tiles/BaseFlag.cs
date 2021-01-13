using TurtleChallenge.Assets.Contracts.Tiles;

namespace TurtleChallenge.Assets.Implementation.Tiles
{
    public class BaseFlag : BaseTile, IFlag
    {
        public override string Icon()
        {
            return "☺";
        }
    }
}
