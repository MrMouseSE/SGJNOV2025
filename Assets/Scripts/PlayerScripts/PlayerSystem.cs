using System;
using GameSceneScripts.TilesGeneratorScripts;
using MouseCursorScripts;

namespace PlayerScripts
{
    public class PlayerSystem : IGameSystem, IDisposable
    {
        private PlayerModel _model;
        private PlayerContainer _playerContainer;
        private GameContext _gameContext;
        
        private bool _isInitialized;
        
        public PlayerSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void InitGameSystem()
        {
            _isInitialized = true;
            _playerContainer = _gameContext.PlayerContainer;
            _model = new PlayerModel(_playerContainer, _gameContext);
            _gameContext.InitializeSystemByType(typeof(MouseCursorSystem));
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            if (!_isInitialized) return;
            _isInitialized = true;
            _model.PlayerMover.Move();
            _model.ShowTrajectory();
        }

        public void Dispose()
        {
            _model.Dispose();
        }
    }
}