using System.Media;
using System.Reflection;
using TurtleChallenge.Assets.Contracts.Sounds;

namespace TurtleChallenge.Assets.Implementaation.Sounds
{
    public class DefaultSound : ISound
    {
        public void Play(string fileName)
        {
            new SoundPlayer
            {
                SoundLocation = fileName
            }.Play();
        }
    }
}
