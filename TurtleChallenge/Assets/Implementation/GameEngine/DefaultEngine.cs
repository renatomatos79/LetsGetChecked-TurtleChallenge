using System;
using System.Linq;
using TurtleChallenge.Assets.Contracts.GameEngine;
using TurtleChallenge.Assets.Contracts.Settings;
using TurtleChallenge.Assets.Contracts.Sounds;
using TurtleChallenge.Assets.Contracts.Tiles;
using TurtleChallenge.Assets.Implementation.Tiles;
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
            this.Level = 1;
            this.Active = false;
            this.Player.OnChange += (evt) =>
            {
                this.Notify(evt);
            };
        }

        public IPlayer Player { get; }
        public ISettings Settings { get; }
        public bool Active { get; internal set; }
        public int Level { get; internal set; }

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
            this.Active = true;
            this.Player.Direction = Settings.PlayerSettings.Direction;
            this.Player.Position = Settings.PlayerSettings.Position;
        }

        private void Victory()
        {
            var rndX = new Random();
            var rndY = new Random();

            this.Level++;
            this.Player.Direction = Settings.PlayerSettings.Direction;
            this.Player.Position = Settings.PlayerSettings.Position;

            this.Player.Board.Exit = new BaseFlag
            {
                Position = new Structs.Position
                {
                    X = rndX.Next(1, this.Player.Board.Width - 1),
                    Y = rndY.Next(1, this.Player.Board.Height - 1)
                }
            };

            this.Player.Board.Enemies.Clear();
            for (var i = 1; i <= this.Level + 2; i++)
            {
                var pos = new Structs.Position { X = rndX.Next(1, this.Player.Board.Width - 1) };
                while (pos.Equals(this.Player.Board.Exit.Position))
                {
                    pos = new Structs.Position 
                    { 
                        X = rndX.Next(1, this.Player.Board.Width - 1),
                        Y = rndY.Next(1, this.Player.Board.Height - 1)
                    };
                }
                var enemy = new BaseEnemy { Position = pos };
                this.Player.Board.Enemies.Add(enemy);
            }

            sound.Play(this.Settings.SoundSettings.VictoryFileName);
        }

        public void GameOver()
        {
            this.Active = false;
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

        public ITile Get(int x, int y)
        {
            if (this.Player.Position.Equals(x, y))
            {
                return this.Player;
            }
            if (this.Player.Board.Exit.Position.Equals(x, y))
            {
                return this.Player.Board.Exit;
            }
            if (this.Player.Board.Enemies != null && this.Player.Board.Enemies.Any())
            {
                var enemy = this.Player.Board.Enemies.FirstOrDefault(f => f.Position.Equals(x, y));
                if (enemy != null)
                {
                    return enemy;
                }
            }
            return null;
        }

        public void Do(string action)
        {
            if (action.ToUpper() == "P")
            {
                Play();
            }
            else if (!this.Active)
            {
                sound.Play(this.Settings.SoundSettings.WrongMovementFileName);
            }
            else
            { 
                if (action.ToUpper() == "M")
                {
                    this.Player.Move();
                }
                if (action.ToUpper() == "T")
                {
                    this.Player.Turn();
                }
            }
        }
    }
}
