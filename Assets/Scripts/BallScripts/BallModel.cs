using System;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace BallScripts
{
    public class BallModel : IDisposable
    {
        private readonly BallContainer _container;
        
        public float Velocity;
        public Vector3 Direction { get; private set; } = new Vector3(0,0,0);
        public Vector3 Position { get; private set; }

        public BallModel(BallContainer container)
        {
            _container = container;
            _container.E_collisionEntered += TryRebound;
        }

        public void Dispose()
        {
            _container.E_collisionEntered -= TryRebound;
        }

        public void Move(float deltaTime)
        {
            Position += Direction * (deltaTime * Velocity);
            _container.Transform.position = Position;
        }

        public void SetDirection(Vector3 direction)
        {
            Direction = direction.normalized;
        }

        private void TryRebound(Collider other)
        {
            if (other.gameObject.TryGetComponent(out SimpleTileContainer tileContainer))
            {
                Vector3 newDirection = new Vector3(tileContainer.TileModel.GetDirection(this, other).x, 0, tileContainer.TileModel.GetDirection(this, other).z);
                Direction = newDirection.normalized;
            }
        }
    }
}