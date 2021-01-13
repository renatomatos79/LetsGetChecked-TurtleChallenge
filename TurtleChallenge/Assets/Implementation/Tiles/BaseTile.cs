using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Implementation.Tiles
{
    public abstract class BaseTile : ITile
    {
        public Position Position { get; set; }
        public abstract string Icon();
    }
}
