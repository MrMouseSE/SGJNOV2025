using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts
{
    public class TileNormalModel : ITileModel
    {
        private readonly AbstractTileContainer _container;

        public TileNormalModel(AbstractTileContainer container)
        {
            _container = container;
        }

        public Vector3 GetDirection(BallModel ballModel, Collider touchingCollider)
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