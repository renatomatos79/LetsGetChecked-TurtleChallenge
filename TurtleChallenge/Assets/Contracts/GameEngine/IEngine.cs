using TurtleChallenge.Assets.Contracts.Settings;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Assets.Contracts.GameEngine
{
    public interface IEngine
    {
        IPlayer Player { get; }
        ISettings Settings { get; }
        void Notify(EngineEvent engineEvent);
    }
}
