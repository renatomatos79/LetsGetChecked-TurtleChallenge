using System.Collections.Generic;
using TurtleChallenge.Assets.Implementation.Tiles;

namespace TurtleChallenge.Assets.Implementation.Settings
{
    public class BoardSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BaseEnemy> Enemies { get; set; }
        public BaseFlag Exit { get; set; }
    }
}
