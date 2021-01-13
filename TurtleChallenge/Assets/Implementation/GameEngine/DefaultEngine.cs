using System;
using TurtleChallenge.Assets.Contracts.GameEngine;
using TurtleChallenge.Assets.Contracts.Settings;
using TurtleChallenge.Assets.Contracts.Sounds;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Assets.Implementation.GameEngine
{
    public class DefaultEngine : IEngine
    {
        private readonly ISound sound;

        public DefaultEngine(IPlayer player, ISettings settings, ISound sound)
        {
            this.Player = player;
            this.Settings = settings;
            this.sound = sound;
            this.Player.OnChange += (evt) =>
            {
                this.Notify(evt);
            };
        }

        public IPlayer Player { get; }
        public ISettings Settings { get; }

        public void Notify(EngineEvent engineEvent)
        {
            switch (engineEvent)
            {
                case EngineEvent.Play:
                    this.Play();
                    break;
                case EngineEvent.GameOver:
                    this.GameOver();
                    break;
                case EngineEvent.Victory:
                    this.Victory();
                    break;
                case EngineEvent.InvalidMovement:
                    this.InvalidMovement();
                    break;
                case EngineEvent.PlayerMovement:
                    this.PlayMovement();
                    break;
                case EngineEvent.PlayerTurnAround:
                    this.PlayMovement();
                    break;
            }
        }

        private void Play()
        {
            this.Player.Direction = Settings.PlayerSettings.Direction;
            this.Player.Position = Settings.PlayerSettings.Position;
        }

        private void Victory()
        {
            sound.Play(this.Settings.SoundSettings.VictoryFileName);
        }

        public void GameOver()
        {
            sound.Play(this.Settings.SoundSettings.GameOverFileName);
        }

        private void InvalidMovement()
        {
            sound.Play(this.Settings.SoundSettings.WrongMovementFileName);
        }

        private void PlayMovement()
        {
            sound.Play(this.Settings.SoundSettings.PlayMovementFileName);
        }
    }
}
