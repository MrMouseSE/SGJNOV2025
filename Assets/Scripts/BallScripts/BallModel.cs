using System;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace BallScripts
{
    public class BallModel : IDisposable
    {
        private readonly BallContainer _container;
        
        public float Velocity;

        public BallModel(BallContainer container)
        {
            _container = container;
            _container.E_collisionEntered += OnECollisionEntered;
        }

        public void Dispose()
        {
            _container.E_collisionEntered -= OnECollisionEntered;
        }

        public void Move(float deltaTime)
        {
            _container.Position += _container.Direction * (deltaTime * Velocity);
            _container.Transform.position = _container.Position;
        }

        private void OnECollisionEntered(Collider other)
        {
            if (other.gameObject.TryGetComponent(out SimpleTileContainer tileContainer))
            {
                Vector3 newDirection = new Vector3(tileContainer.TileModel.GetDirection(_container, other).x, 0, tileContainer.TileModel.GetDirection(_container, other).z);
                _container.Direction = newDirection.normalized;
            }
        }
    }
}