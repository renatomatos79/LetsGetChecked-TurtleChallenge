using System.Collections.Generic;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Enums;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Implementation.Settings
{
    public class BoardSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<IEnemy> Enemies { get; set; }
        public IFlag Exit { get; set; }
    }
}
