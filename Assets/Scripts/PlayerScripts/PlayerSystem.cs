using System;

namespace PlayerScripts
{
    public class PlayerSystem : IGameSystem, IDisposable
    {
        private readonly PlayerModel _model;
        private readonly PlayerContainer _playerContainer;
        
        public PlayerSystem(PlayerContainer playerContainer, GameContext gameContext)
        {
            _playerContainer = playerContainer;
            _model = new PlayerModel(_playerContainer, gameContext);
        }

        public void InitGameSystem()
        { }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            _model.PlayerMover.Move();
            _model.ShowTrajectory();
        }

        public void Dispose()
        {
            _model.Dispose();
        }
    }
}