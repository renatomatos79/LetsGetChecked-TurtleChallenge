using System;
using TurtleChallenge.Assets.Contracts.Boards;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Assets.Contracts.Tiles
{
    public interface IPlayer : ITile
    {
        IBoard Board { get; }
        Direction Direction { get; set; }
        Action<EngineEvent> OnChange { get; set; }
        IPlayer Turn();
        IPlayer Move();
    }
}
