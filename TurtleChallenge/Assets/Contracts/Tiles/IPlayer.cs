using System;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Assets.Contracts.Tiles
{
    public interface IPlayer : ITile
    {
        Direction Direction { get; set; }
        Action<EngineEvent> OnChange { get; set; }
        IPlayer Turn();
        IPlayer Move();
    }
}
