using TurtleChallenge.Assets.Contracts.Settings;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Assets.Contracts.GameEngine
{
    public interface IEngine
    {
        bool Active { get; }
        int Level { get; }
        IPlayer Player { get; }
        ISettings Settings { get; }
        void Notify(EngineEvent engineEvent);
        ITile Get(int x, int Y);
        void Do(EngineOperation action);
    }
}
