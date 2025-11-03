using System;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace BallScripts
{
    public class BallModel : IDisposable
    {
        public float Velocity;
        public Vector3 Direction { get; private set; }
        public Vector3 PreviousDirection;
        public Vector3 Position { get; private set; }
        public int Bounces { get; }

        private readonly BallContainer _container;
        public BallSystem BallSystem;
        private GameContext _gameContext;
        private int _currentBounce;
        public bool IsDupThisBounce;

        public BallModel(BallSystem ballSystem,BallContainer container, GameContext gameContext)
        {
            BallSystem = ballSystem;
            _gameContext = gameContext;
            _container = container;
            _container.E_collisionEntered += TryRebound;
            Bounces = container.Bounces;
        }

        public void RestoreCurrentBounce()
        {
            _currentBounce = 0;
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

        public void SetPosition(Vector3 position)
        {
            _container.Transform.position = position;
            Position = position;
        }
        public void SetDirection(Vector3 direction)
        {
            Direction = direction.normalized;
        }

        public void DestroyBall()
        {
            _container.BallDestroyParticlesTransform.parent = null;
            _container.BallDestroyParticleSystem.Play();
            UnityEngine.Object.Destroy(_container.BallGameObject);
            ((BallsSystems)_gameContext.GetGameSystemByType(typeof(BallsSystems))).Model.RemoveBallSystem(BallSystem);
            Dispose();
        }

        private void TryRebound(Collider other)
        {
            PreviousDirection = Direction;
            if (_currentBounce >= Bounces)
            {
                DestroyBall();
                return;
            }
            IsDupThisBounce = false;
            
            if (other.TryGetComponent(out AbstractTileContainer tileContainer))
            {
                Direction = tileContainer.TileModel.GetDirection(this, other).normalized;
                tileContainer.TileModel.InteractByBall(this, other);
                tileContainer.StartGlowUpTileAnimation();
            }
            else if (other.GetComponentInParent<AbstractTileContainer>() != null)
            {
                tileContainer = other.GetComponentInParent<AbstractTileContainer>();
                Direction = tileContainer.TileModel.GetDirection(this, other).normalized;
                tileContainer.TileModel.InteractByBall(this, other);
                tileContainer.StartGlowUpTileAnimation();
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