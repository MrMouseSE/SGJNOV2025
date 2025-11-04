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

        public override Vector3 GetDirection(Vector3 direction, Vector3 position, Collider touchedCollider)
        {
            return ShootTheBall(direction, position, touchedCollider);
        }

        private Vector3 ShootTheBall(Vector3 direction, Vector3 position, Collider touchedCollider)
        {
            if (!_container.Colliders.Contains(touchedCollider)) return direction;
    
            Vector3 closestPoint = touchedCollider.ClosestPoint(position);
    
            Vector3 collisionNormal = (position - closestPoint).normalized;
    
            if (collisionNormal == Vector3.zero)
                collisionNormal = (position - _container.transform.position).normalized;
            return collisionNormal;
        }
    }
}