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

        private bool _isCanBeMoved;

        public bool CheckMoveAvailability()
        {
            return _isCanBeMoved;
        }

        public void SetMoveByPlayerAvailability(bool isCanBeMoved)
        {
            _isCanBeMoved = isCanBeMoved;
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
        }

        public void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
            ballModel.RestoreCurrentBounce();
        }

        public Vector3 GetDirection(Vector3 direction, Vector3 position, Collider touchedCollider)
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