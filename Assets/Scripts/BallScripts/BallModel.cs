using System;
using TileScript;
using UnityEngine;

namespace BallScripts
{
    public class BallModel : IDisposable
    {
        private readonly BallContainer _container;

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
            _container.Position += _container.Direction * (deltaTime * _container.Velocity);
            _container.Transform.position = _container.Position;
        }

        private void OnECollisionEntered(Collider other)
        {
            if (other.gameObject.TryGetComponent(out TileContainer tileContainer))
            {
                Vector3 newDirection = new Vector3(tileContainer.TileModel.GetDirection(_container).x, 0, tileContainer.TileModel.GetDirection(_container).z);
                _container.Direction = newDirection.normalized;
            }
        }
    }
}