namespace TurtleChallenge.Assets.Implementation.Settings
{
    public class DefaultSettings : Contracts.Settings.ISettings
    {
        public PlayerSettings PlayerSettings { get; set; }
        public SoundSettings SoundSettings { get; set; }
        public BoardSettings BoardSettings { get; set; }
    }    
}
