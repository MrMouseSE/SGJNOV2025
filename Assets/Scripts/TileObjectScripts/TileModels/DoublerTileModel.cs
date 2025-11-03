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
        
        public DoublerTileModel(AbstractTileContainer container, GameContext gameContext) : base(container)
        {
            _container = container;
            _gameContext = gameContext;
        }
        
        private bool _isCanBeMoved;

        public bool CheckMoveAvailability()
        {
            return _isCanBeMoved;
        }

        public void SetMoveByPlayerAvailability(bool isCanBeMoved)
        {
            _isCanBeMoved = isCanBeMoved;
        }

        public override void UpdateModel(float deltaTime, GameContext gameContext)
        {
        }

        public override void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
            ballModel.IsDupThisBounce = true;
            var handler = (GameSceneHandler)_gameContext.SceneHandler.GetSceneHandlerByName(_gameContext.SceneHandler.GameSceneName);
            var ballSystem = _gameContext.BallFactory.CreateBall(handler.TilesHolder);
            
            ballSystem.Model.SetPosition(ballModel.Position);
            ballSystem.Model.IsDupThisBounce = false;
            ballSystem.SetDirection(ReflectBall(ballModel, touchedCollider));
            ballSystem.SetVelocity(_gameContext.PlayerContainer.BallSpeed);
            var ballsSystems = (BallsSystems)_gameContext.GetGameSystemByType(typeof(BallsSystems));
            ballsSystems.Model.AddBallSystem(ballSystem);
        }

        public override Vector3 GetDirection(BallModel ballModel, Collider touchingCollider)
        {
            return ReflectBall(ballModel, touchingCollider);
        }

        private Vector3 ReflectBall(BallModel ballModel, Collider touchingCollider)
        {
            if (!_container.Colliders.Contains(touchingCollider)) return ballModel.Direction;
    
            Vector3 closestPoint = touchingCollider.ClosestPoint(ballModel.Position);
    
            Vector3 collisionNormal = (ballModel.Position - closestPoint).normalized;
    
            if (collisionNormal == Vector3.zero)
                collisionNormal = (ballModel.Position - _container.transform.position).normalized;
    
            return Vector3.Reflect(ballModel.Direction, collisionNormal);
        }
    }
}