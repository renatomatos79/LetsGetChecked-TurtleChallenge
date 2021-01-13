using System;
using System.Linq;
using TurtleChallenge.Assets.Boards;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Assets.Implementaation.Sounds;
using TurtleChallenge.Assets.Implementation.GameEngine;
using TurtleChallenge.Assets.Implementation.Settings;
using TurtleChallenge.Assets.Implementation.Tiles;

namespace TurtleChallenge
{
    class Program
    {
        public static string EXIT = "X";
        public static string[] VALID_OPTIONS = new string[] { "T", "M", EXIT, "P" };

        static string GetMenu()
        {
            Console.Clear();
            Console.WriteLine("T: Turn 90º");
            Console.WriteLine("M: Move");
            Console.WriteLine("X: Quit");
            Console.WriteLine("P: Play");
            Console.Write("=> ");
            var opt = Console.ReadLine().Trim().ToUpper();
            return VALID_OPTIONS.Contains(opt) ? opt : GetMenu();
        }

        static void Draw(DefaultBoard board)
        {
            Console.WriteLine("");
            Console.WriteLine("==========================================");

            for (var x = 0; x <= board.Width - 1; x++)
            {
                for (var y = 0; y <= board.Height - 1; y++)
                {
                    Console.Write("___ ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("==========================================");
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            var defaultSettings = new DefaultSettings
            {
                PlayerSettings = new PlayerSettings
                {
                    Direction = Enums.Direction.North,
                    Position = new Structs.Position { X = 0, Y = 0 }
                },
                SoundSettings = new SoundSettings
                {
                    Enabled = true,
                    GameOverFileName = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "Sounds", "gameover.wav"),
                    PlayMovementFileName = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "Sounds", "movement.wav"),
                    TurnFileName = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "Sounds", "movement.wav"),
                    VictoryFileName = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "Sounds", "victory.wav"),
                    WrongMovementFileName = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "Sounds", "wrong-movement.wav"),
                }
            };
            var board = new DefaultBoard
            {
                Width = 5,
                Height = 4,
                Exit = new BaseFlag { Position = new Structs.Position { X = 4, Y = 2 } },
                Enemies = new System.Collections.Generic.List<IEnemy>
                {
                    new BaseEnemy { Position = new Structs.Position { X = 0, Y = 1 } },
                    new BaseEnemy { Position = new Structs.Position { X = 3, Y = 1 } },
                    new BaseEnemy { Position = new Structs.Position { X = 3, Y = 3 } },
                }
            };

            var player = new Player(board);

            var defaultSound = new DefaultSound();

            var defaultEngine = new DefaultEngine(player, defaultSettings, defaultSound);

            var opt = GetMenu();
            while (opt != EXIT)
            {
                Draw(board);

                opt = GetMenu();
            }
            
            Console.WriteLine("Hello World!");
        }
    }
}
