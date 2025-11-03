using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts
{
    public class DefaultTileModel : ITileModel
    {
        private readonly AbstractTileContainer _container;

        public DefaultTileModel(AbstractTileContainer container)
        {
            _container = container;
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

        public virtual void UpdateModel(float deltaTime, GameContext gameContext)
        {
        }

        public void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
        }

        public virtual Vector3 GetDirection(BallModel ballModel, Collider touchingCollider)
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