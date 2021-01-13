using System;
using System.Linq;
using TurtleChallenge.Assets.Boards;
using TurtleChallenge.Assets.Contracts.GameEngine;
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

        static void Draw(IEngine engine)
        {
            Console.WriteLine($"Level: {engine.Level} ");
            Console.WriteLine("==========================================");

            if (!engine.Active)
            {
                Console.WriteLine("");
                Console.WriteLine("Game Over");
                Console.WriteLine("");
            }
            else
            {
                for (var y = 0; y < engine.Player.Board.Height; y++)
                {
                    for (var x = 0; x < engine.Player.Board.Width; x++)
                    {
                        Console.Write($"   {engine.Get(x, y)?.Icon() ?? " "}   |");
                    }
                    Console.WriteLine();
                    Console.WriteLine("------------------------------------------");
                }
            }
            

            Console.WriteLine("==========================================");
            Console.WriteLine("");
        }

        static string GetMenu(IEngine engine)
        {
            Console.Clear();

            Draw(engine);

            Console.WriteLine("T: Turn 90º");
            Console.WriteLine("M: Move");
            Console.WriteLine("X: Quit");
            Console.WriteLine("P: Play");
            Console.Write("=> ");            

            var opt = Console.ReadLine().Trim().ToUpper();
            return VALID_OPTIONS.Contains(opt) ? opt : GetMenu(engine);
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
                Width = 6,
                Height = 6,
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
            defaultEngine.Notify(Enums.EngineEvent.Play);

            var opt = GetMenu(defaultEngine);
            while (opt != EXIT)
            {
                defaultEngine.Do(opt);

                opt = GetMenu(defaultEngine);
            }
            
            Console.WriteLine("The end!");
        }
    }
}
