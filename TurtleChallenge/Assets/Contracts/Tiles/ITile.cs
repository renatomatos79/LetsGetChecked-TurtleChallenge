using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Contracts.Tiles
{
    public interface ITile
    {
        Position Position { get; set; }
    }
}
