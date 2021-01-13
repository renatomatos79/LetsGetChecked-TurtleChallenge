using Newtonsoft.Json;
using System;
using System.Linq;
using TurtleChallenge.Assets.Contracts.Settings;
using TurtleChallenge.Assets.Implementation.Settings;

namespace TurtleChallenge.Helper
{
    public static class SettingsHelper
    {
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

        public static ISettings GetSettings(string[] args)
        {
            try
            {
                var json = GetJsonSettings(args);
                return JsonConvert.DeserializeObject<DefaultSettings>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! :(  there was a little problem during the game! please restart the game.");
                Console.WriteLine($"ERROR => {ex}");
                return null;
            }
        }
    }
}
