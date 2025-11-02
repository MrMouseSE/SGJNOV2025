using BallScripts;
using UnityEngine;

namespace TileScript
{
    public class TileModelNormalModel : ITileModel
    {
        private readonly TileContainer _container;

        public TileModelNormalModel(TileContainer container)
        {
            _container = container;
        }

        public Vector3 GetDirection(BallContainer ball)
        {
            return ReflectBall(ball);
        }

        private Vector3 ReflectBall(BallContainer ball)
        {
            if (_container.Collider == null) return ball.Direction;
    
            Vector3 closestPoint = _container.Collider.ClosestPoint(ball.Position);
    
            Vector3 collisionNormal = (ball.Position - closestPoint).normalized;
    
            if (collisionNormal == Vector3.zero)
                collisionNormal = (ball.Position - _container.transform.position).normalized;
    
            return Vector3.Reflect(ball.Direction, collisionNormal);
        }
    }
}