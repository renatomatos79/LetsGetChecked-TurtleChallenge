using TurtleChallenge.Assets.Contracts.Boards;
using TurtleChallenge.Assets.Contracts.Flags;
using TurtleChallenge.Enums;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Contracts.Players
{
    public interface IPlayer : ITile
    {
        IBoard Board { get; }
        Direction Direction { get; set; }
        IPlayer Turn();
        IPlayer Move();
    }
}
