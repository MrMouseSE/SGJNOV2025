using System;
using TileObjectScripts.TileContainers;
using UnityEngine;
using UnityEngine.Android;

namespace BallScripts
{
    public class BallModel : IDisposable
    {
        public float Velocity;
        public Vector3 Direction { get; private set; }
        public Vector3 Position { get; private set; }
        public int Bounces { get; }

        private readonly BallContainer _container;
        private int _currentBounce;

        public BallModel(BallContainer container)
        {
            _container = container;
            _container.E_collisionEntered += TryRebound;
            Bounces = container.Bounces;
        }

        public void Dispose()
        {
            _container.E_collisionEntered -= TryRebound;
        }

        public void Move(float deltaTime)
        {
            _container.Transform.position += Direction * (Velocity * deltaTime);
            Position = _container.Transform.position;
        }

        public void SetDirection(Vector3 direction)
        {
            Direction = direction.normalized;
        }

        private void TryRebound(Collider other)
        {
            if (_currentBounce >= Bounces)
            {
                //TODO : Destroy ball
                return;
            }

            
            if (other.TryGetComponent(out AbstractTileContainer tileModel))
            {
                Direction = tileModel.TileModel.GetDirection(this, other).normalized;

                if (tileModel.IsGlowing == false)
                {
                    tileModel.StartGlowUpTileAnimation();
                }
            }
            else if (other.TryGetComponent(out Collider collider))
            {
                Vector3 normal = (other.ClosestPoint(_container.Transform.position) - _container.Transform.position).normalized;
                Direction = Vector3.Reflect(Direction, normal).normalized;
            }

            _currentBounce++;
        }
    }
}