using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class CannonTileModel : DefaultTileModel
    {
        private readonly AbstractTileContainer _container;

        public CannonTileModel(AbstractTileContainer container) : base(container)
        {
            _container = container;
        }

        public override void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
            ballModel.RestoreCurrentBounce();
        }

        public override Vector3 GetDirection(BallModel ballModel, Collider touchedCollider)
        {
            return ShootTheBall(ballModel, touchedCollider);
        }

        private Vector3 ShootTheBall(BallModel ballModel, Collider touchedCollider)
        {
            if (!_container.Colliders.Contains(touchedCollider)) return ballModel.Direction;
    
            Vector3 closestPoint = touchedCollider.ClosestPoint(ballModel.Position);
    
            Vector3 collisionNormal = (ballModel.Position - closestPoint).normalized;
    
            if (collisionNormal == Vector3.zero)
                collisionNormal = (ballModel.Position - _container.transform.position).normalized;
            return collisionNormal;
        }
    }
}