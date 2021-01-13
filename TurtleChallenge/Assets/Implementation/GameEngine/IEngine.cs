using System;
using TurtleChallenge.Assets.Contracts.Boards;
using TurtleChallenge.Assets.Contracts.GameEngine;
using TurtleChallenge.Assets.Contracts.Players;
using TurtleChallenge.Assets.Contracts.Settings;
using TurtleChallenge.Assets.Sounds;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Assets.Implementation.GameEngine
{
    public class DefaultEngine : IEngine
    {
        private readonly IPlayer player;
        private readonly IBoard board;
        private readonly ISettings settings;
        private readonly ISound sound;

        public DefaultEngine(IPlayer player, IBoard board, ISettings settings, ISound sound)
        {
            this.player = player;
            this.board = board;
            this.settings = settings;
            this.sound = sound;
        }

        public IPlayer Player => player;
        public IBoard Board => board;
        public ISettings Settings => settings;

        public void GameOver()
        {
            sound.Play(this.settings.SoundSettings.GameOverFileName);
        }

        public void InvalidMovement()
        {
            sound.Play(this.settings.SoundSettings.WrongMovementFileName);
        }

        public void Notify(EngineEvent engineEvent)
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            this.player.Direction = settings.PlayerSettings.Direction;
            this.player.Position = settings.PlayerSettings.Position;

        }

        public void Victory()
        {
            sound.Play(this.settings.SoundSettings.VictoryFileName);
        }
    }
}
