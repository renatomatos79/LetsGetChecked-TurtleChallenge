using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Assets.Contracts.Enemies;
using TurtleChallenge.Assets.Contracts.Flags;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Contracts.Boards
{
    public interface IBoard
    {
        int Width { get; set; }
        int Height { get; set; }
        List<IEnemy> Enemies { get; set; }
        IFlag Exit { get; set; }
        bool IsValid(Position position);
        ITile Colision(Position position);
    }
}
