using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurtleChallenge.Assets.Contracts.Boards;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Assets.Implementation.Settings;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Boards
{
    public class DefaultBoard : IBoard
    {
        public DefaultBoard() { }
        
        public int Width { get; set; }
        public int Height { get; set; }
        public List<IEnemy> Enemies { get; set; }
        public IFlag Exit { get; set; }
        public ITile Colision(Position position)
        {
            if (Exit.Position.Equals(position))
            {
                return Exit;
            }
            else if (this.Enemies != null && this.Enemies.Any())
            {
                var enemy = this.Enemies.FirstOrDefault(f => f.Position.Equals(position));
                if (enemy != null)
                {
                    return enemy;
                }
            }
            return null;
        }

        public bool IsValid(Position position)
        {
            return (position.X >= 0 && position.X <= (Width - 1)) &&
                   (position.Y >= 0 && position.Y <= (Height - 1));
        }
    }
}
