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

        public virtual Vector3 GetDirection(Vector3 direction, Vector3 position, Collider touchingCollider)
        {
            Vector3 hitNormal = (position - touchingCollider.transform.position).normalized;
            return Vector3.Reflect(direction, hitNormal).normalized;
        }
    }
}