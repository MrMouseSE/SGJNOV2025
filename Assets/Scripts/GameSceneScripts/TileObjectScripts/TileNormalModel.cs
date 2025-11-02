using BallScripts;
using GameSceneScripts.TileObjectScripts.TileContainers;
using UnityEngine;

namespace GameSceneScripts.TileObjectScripts
{
    public class TileNormalModel : ITileModel
    {
        private readonly AbstractTileContainer _container;

        public TileNormalModel(AbstractTileContainer container)
        {
            _container = container;
        }

        public Vector3 GetDirection(BallContainer ball, Collider touchingCollider)
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