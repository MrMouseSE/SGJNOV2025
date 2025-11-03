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

        public virtual Vector3 GetDirection(BallContainer ball, Collider touchingCollider)
        {
            return ReflectBall(ball, touchingCollider);
        }

        private Vector3 ReflectBall(BallContainer ball, Collider touchingCollider)
        {
            if (!_container.Colliders.Contains(touchingCollider)) return ball.Direction;
    
            Vector3 closestPoint = touchingCollider.ClosestPoint(ball.Position);
    
            Vector3 collisionNormal = (ball.Position - closestPoint).normalized;
    
            if (collisionNormal == Vector3.zero)
                collisionNormal = (ball.Position - _container.transform.position).normalized;
    
            return Vector3.Reflect(ball.Direction, collisionNormal);
        }
    }
}