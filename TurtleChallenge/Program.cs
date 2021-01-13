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

        static void Main(string[] args)
        {
            var defaultSettings = SettingsHelper.GetSettings(args);
            if (defaultSettings == null)
            {
                Console.WriteLine("Oops! :(  The settings file could not be loaded!");
            }
            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! :(  there was a little problem during the game! please restart the game.");
                Console.WriteLine($"ERROR => {ex}");
            }
            Console.WriteLine("The end!");
        }
    }
}
