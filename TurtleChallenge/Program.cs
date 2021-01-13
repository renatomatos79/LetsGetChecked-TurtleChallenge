using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Linq;
using TurtleChallenge.Assets.Boards;
using TurtleChallenge.Assets.Contracts.GameEngine;
using TurtleChallenge.Assets.Contracts.Settings;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Assets.Implementaation.Sounds;
using TurtleChallenge.Assets.Implementation.GameEngine;
using TurtleChallenge.Assets.Implementation.Settings;
using TurtleChallenge.Assets.Implementation.Tiles;
using TurtleChallenge.Helper;

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

        private static string GetJsonSettings(string[] args)
        {
            var fileName = string.Empty;
            if (args.Contains("--settings="))
            {
                var settings = args.FirstOrDefault(f => f.StartsWith("--settings="));
                var parts = settings.Split("=");
                if (parts.Length == 2)
                {
                    if (System.IO.File.Exists(parts[1]))
                    {
                        fileName = System.IO.File.ReadAllText(parts[1]);
                    }
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "Settings", "default.json");
            }            
            return System.IO.File.ReadAllText(fileName);
        }

        private static ISettings GetSettings(string[] args)
        {
            var json = GetJsonSettings(args);
            return JsonConvert.DeserializeObject<DefaultSettings>(json);
        }

        static void Main(string[] args)
        {
            var defaultSettings = GetSettings(args);
            var json = JsonConvert.SerializeObject(defaultSettings);
            System.IO.File.WriteAllText("c:\\temp\\settings.json", json);
            Console.WriteLine(json);

            var board = MapperHelper.Mapper.Map<DefaultBoard>(defaultSettings.BoardSettings);
            var player = new Player(board);
            var defaultSound = new DefaultSound();
            var defaultEngine = new DefaultEngine(player, defaultSettings, defaultSound);
            defaultEngine.Notify(Enums.EngineEvent.Play);

            var opt = GetMenu(defaultEngine);
            while (opt != EXIT)
            {
                var engineOperation = EngineOperationHelper.From(opt);
                if (engineOperation.HasValue)
                {
                    defaultEngine.Do(engineOperation.Value);
                }               
                opt = GetMenu(defaultEngine);
            }
            
            Console.WriteLine("The end!");
        }
    }
}
