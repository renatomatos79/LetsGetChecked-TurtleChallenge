using System;
using TurtleChallenge.Assets.Contracts.Boards;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Enums;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Implementation.Tiles
{
    public class Player : BaseTile, IPlayer
    {
        public Player(IBoard board) => this.Board = board;
        
        public Direction Direction { get; set; }
        public Action<EngineEvent> OnChange { get; set; }
        public IBoard Board { get; internal set; }

        private void Change(EngineEvent engineEvent)
        {
            if (OnChange != null)
            {
                OnChange.Invoke(engineEvent);
            }
        }

        public IPlayer Move()
        {
            var nextMovement = NextMovement();
            if (!Board.IsValid(nextMovement))
            {
                Change(EngineEvent.InvalidMovement);
            }
            else
            {
                this.Position = nextMovement;
                var tile = Board.Colision(this.Position);
                if (tile != null)
                {
                    if (tile is IEnemy)
                    {
                        Change(EngineEvent.GameOver);
                    }
                    else 
                    {
                        Change(EngineEvent.Victory);
                    }
                }
                else
                {
                    Change(EngineEvent.PlayerMovement);
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
            this.Change(EngineEvent.PlayerTurnAround);
            return this;
        }

        private Position NextMovement()
        {
            int x = this.Position.X;
            int y = this.Position.Y;
            switch (this.Direction)
            {
                case Direction.North:
                    {
                        y--;
                        break;
                    }
                case Direction.East:
                    {
                        x++;
                        break;
                    }
                case Direction.South:
                    {
                        y++;
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

        public override string Icon()
        {
            switch (this.Direction)
            {
                case Direction.North:
                    return "↑";
                case Direction.East:
                    return ">";
                case Direction.South:
                    return "↓";
                case Direction.West:
                    return "<";
            }
            return string.Empty;
        }
    }
}
