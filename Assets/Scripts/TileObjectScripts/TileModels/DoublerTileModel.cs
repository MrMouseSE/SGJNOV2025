using System.Collections.Generic;
using BallScripts;
using GameSceneScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class DoublerTileModel : DefaultTileModel
    {
        private AbstractTileContainer _container;
        private GameContext _gameContext;
        
        private Dictionary<BallSystem, int> _clonedBalls = new Dictionary<BallSystem, int>();
        
        public DoublerTileModel(AbstractTileContainer container, GameContext gameContext) : base(container)
        {
            _container = container;
            _gameContext = gameContext;
        }
        
        public override void UpdateModel(float deltaTime, GameContext gameContext)
        {
        }

        public override void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
            if (_clonedBalls.ContainsKey(ballModel.BallSystem) && _clonedBalls[ballModel.BallSystem] > _gameContext.LevelDescription.CloneCount)
            {
                return;
            }
            
            ballModel.IsDupThisBounce = true;
            var handler = (GameSceneHandler)_gameContext.SceneHandler.GetSceneHandlerByName(_gameContext.SceneHandler.GameSceneName);
            var ballSystem = _gameContext.BallFactory.CreateBall(handler.TilesHolder);
            
            ballSystem.Model.SetPosition(ballModel.Position);
            ballSystem.Model.IsDupThisBounce = true;
            ballSystem.SetDirection(-ballModel.PreviousDirection);
            ballSystem.SetVelocity(_gameContext.PlayerContainer.BallSpeed);
            var ballsSystems = (BallsSystems)_gameContext.GetGameSystemByType(typeof(BallsSystems));
            ballsSystems.Model.AddBallSystem(ballSystem);

            if (!_clonedBalls.TryAdd(ballModel.BallSystem, 1))
            {
                _clonedBalls[ballModel.BallSystem]++;
            }
            _clonedBalls.Add(ballSystem, _clonedBalls[ballModel.BallSystem]);
        }
    }
}