using TurtleChallenge.Enums;
using TurtleChallenge.Structs;

namespace TurtleChallenge.Assets.Implementation.Settings
{
    public class SoundSettings
    {
        public bool Enabled { get; set; }
        public string WrongMovementFileName { get; set; }
        public string PlayMovementFileName { get; set; }
        public string TurnFileName { get; set; }
        public string GameOverFileName { get; set; }
        public string VictoryFileName { get; set; }
    }
}
