using System;

namespace PlayerScripts
{
    public class PlayerSystem : IGameSystem, IDisposable
    {
        private readonly PlayerModel _model;
        private readonly PlayerContainer _playerContainer;
        
        public PlayerSystem(InputSystemActions inputSystem, PlayerContainer playerContainer, GameContext gameContext)
        {
            _playerContainer = playerContainer;
            _model = new PlayerModel(inputSystem, _playerContainer, gameContext);
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