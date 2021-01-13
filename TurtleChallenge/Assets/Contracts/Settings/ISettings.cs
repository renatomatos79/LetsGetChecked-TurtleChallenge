using TurtleChallenge.Assets.Implementation.Settings;

namespace TurtleChallenge.Assets.Contracts.Settings
{
    public interface ISettings
    {
        PlayerSettings PlayerSettings { get; set; }
        SoundSettings SoundSettings { get; set; }
    }    
}
