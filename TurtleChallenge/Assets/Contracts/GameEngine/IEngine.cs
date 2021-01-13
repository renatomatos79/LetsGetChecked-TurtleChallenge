using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Assets.Contracts.Boards;
using TurtleChallenge.Assets.Contracts.Players;
using TurtleChallenge.Assets.Contracts.Settings;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Assets.Contracts.GameEngine
{
    public interface IEngine
    {
        IPlayer Player { get; }
        IBoard Board { get; }
        ISettings Settings { get; }
        void Notify(EngineEvent engineEvent);
    }
}
