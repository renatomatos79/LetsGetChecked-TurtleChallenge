using TurtleChallenge.Assets.Contracts.Tiles;

namespace TurtleChallenge.Assets.Implementation.Tiles
{
    public class BaseEnemy : BaseTile, IEnemy
    {
        public override string Icon()
        {
            return "B";
        }
    }
}
