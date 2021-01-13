using TurtleChallenge.Assets.Contracts.Boards;
using TurtleChallenge.Assets.Contracts.Enemies;
using TurtleChallenge.Assets.Contracts.GameEngine;
using TurtleChallenge.Assets.Contracts.Players;
using TurtleChallenge.Enums;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Players
{
    public class Player : IPlayer
    {
        private readonly IEngine engine;
        public Player(IBoard board, IEngine engine)
        {
            this.Board = board;
            this.engine = engine;
        }
        public Direction Direction { get; set; }
        public Position Position { get; set; }
        public IBoard Board { get; }

        public IPlayer Move()
        {
            var nextMovement = NextMovement();
            if (!Board.IsValid(nextMovement))
            {
                this.engine.Notify(EngineEvent.InvalidMovement);
            }
            else
            {
                this.Position = nextMovement;
                var tile = Board.Colision(this.Position);
                if (tile != null)
                {
                    if (tile is IEnemy)
                    {
                        this.engine.Notify(EngineEvent.GameOver);
                    }
                    else 
                    {
                        this.engine.Notify(EngineEvent.Victory);
                    }
                }
                else
                {
                    this.engine.Notify(EngineEvent.PlayerMovement);
                }
            }
            return this;
        }

        public IPlayer Turn()
        {
            switch (this.Direction)
            {
                case Direction.North:
                    {
                        this.Direction = Direction.East;
                        break;
                    }
                case Direction.East:
                    {
                        this.Direction = Direction.South;
                        break;
                    }
                case Direction.South:
                    {
                        this.Direction = Direction.West;
                        break;
                    }
                default:
                    {
                        this.Direction = Direction.North;
                        break;
                    }
            }
            this.engine.Notify(EngineEvent.PlayerTurnAround);
            return this;
        }

        private Position NextMovement()
        {
            int x = 0;
            int y = 0;
            switch (this.Direction)
            {
                case Direction.North:
                    {
                        y++;
                        break;
                    }
                case Direction.East:
                    {
                        x++;
                        break;
                    }
                case Direction.South:
                    {
                        y--;
                        break;
                    }
                case Direction.West:
                    {
                        x--;
                        break;
                    }
            }
            return new Position { X = x, Y = y };
        }
    }
}
