using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class CannonTileModel : ITileModel
    {
        private readonly AbstractTileContainer _container;

        public CannonTileModel(AbstractTileContainer container)
        {
            _container = container;
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
        }

        public Vector3 GetDirection(BallContainer ball, Collider touchingCollider)
        {
            //TODO ballModel.RestoreBallBounds;
            return ShootTheBall(ball, touchingCollider);
        }

        private Vector3 ShootTheBall(BallContainer ball, Collider touchingCollider)
        {
            if (!_container.Colliders.Contains(touchingCollider)) return ball.Direction;
    
            Vector3 closestPoint = touchingCollider.ClosestPoint(ball.Position);
    
            Vector3 collisionNormal = (ball.Position - closestPoint).normalized;
    
            if (collisionNormal == Vector3.zero)
                collisionNormal = (ball.Position - _container.transform.position).normalized;
            return collisionNormal;
        }
    }
}