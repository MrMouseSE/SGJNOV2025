using System;
using System.Collections.Generic;
using System.Linq;
using TileObjectScripts;
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
        public Collider Collider => _container.Collider;

        private readonly BallContainer _container;
        public BallSystem BallSystem;
        private GameContext _gameContext;
        private int _currentBounce;
        public bool IsDupThisBounce;
        private TileObjectHandler _previousTouchedTile;

        public BallModel(BallSystem ballSystem,BallContainer container, GameContext gameContext)
        {
            BallSystem = ballSystem;
            _gameContext = gameContext;
            _container = container;
            //_container.E_collisionEntered += TryRebound;
            Bounces = container.Bounces;
        }

        public void RestoreCurrentBounce()
        {
            _currentBounce = 0;
        }

        public void Dispose()
        {
            //_container.E_collisionEntered -= TryRebound;
        }

        public void Move(float deltaTime)
        {
            _container.Transform.position += Direction * (Velocity * deltaTime);
            Position = _container.Transform.position;
            CheckBallCollision();
            if (CheckOutOfGameArea()) DestroyBall();
        }

        public void SetPreviousPosition()
        {
            Position -= Direction * Velocity * Time.deltaTime * 3;
            _container.Transform.position = Position;
        }

        private void CheckBallCollision()
        {
            Dictionary<TileObjectHandler, Collider> touchedTiles = new Dictionary<TileObjectHandler, Collider>();
            foreach (var tileObjectHandler in _gameContext.TileObjectHandlers)
            {
                foreach (var collider in tileObjectHandler.TilePrefab.Colliders)
                {
                    if (collider.bounds.Intersects(_container.Collider.bounds))
                    {
                        touchedTiles.Add(tileObjectHandler, collider);
                    }
                }
            }

            if (touchedTiles.Count == 0) return;
            
            TileObjectHandler touchedTile = touchedTiles.First().Key;
            Collider touchedCollider = touchedTiles.First().Value;
            float distacne = float.MaxValue;

            foreach (var tile in touchedTiles)
            {
                float distanceToCurrentCollider =
                    (_container.Collider.transform.position - tile.Value.transform.position).magnitude;
                if (distanceToCurrentCollider<distacne)
                {
                    distacne = distanceToCurrentCollider;
                    touchedTile = tile.Key;
                    touchedCollider = tile.Value;
                }
            }
            
            if(touchedTile == _previousTouchedTile) return;

            if (Vector3.Dot(touchedCollider.transform.position - Position, Direction) < 0) return;
            float distanceToCollider = (touchedCollider.transform.position - Position).magnitude;
            Vector3 nextPoint = (Position + Direction * Velocity * Time.deltaTime);
            float nextDistance = (touchedCollider.transform.position - nextPoint).magnitude;
            if (nextDistance > distanceToCollider) return;

            _previousTouchedTile = touchedTile;
            TryRebound(touchedCollider);
            
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

        public void DestroyGameObject()
        {
            _container.BallDestroyParticlesTransform.parent = null;
            _container.BallDestroyParticleSystem.Play();
            UnityEngine.Object.Destroy(_container.BallGameObject);
        }

        public void DestroyBall()
        {
            ((BallsSystems)_gameContext.GetGameSystemByType(typeof(BallsSystems))).Model.RemoveBallSystem(BallSystem);
            Dispose();
        }

        private bool CheckOutOfGameArea()
        {
            return Position.x < _gameContext.LevelDescription.LevelPlayebleArea.x ||
                   Position.x > _gameContext.LevelDescription.LevelPlayebleArea.z
                   || Position.z < _gameContext.LevelDescription.LevelPlayebleArea.y ||
                   Position.x > _gameContext.LevelDescription.LevelPlayebleArea.w;
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
                Vector3 hitPoint = other.ClosestPoint(_container.Transform.position);
                Direction = tileContainer.TileModel.GetDirection(Direction, hitPoint, other, this).normalized;

                tileContainer.TileModel.InteractByBall(this, other);

                if (!tileContainer.IsGlowing)
                    tileContainer.StartGlowUpTileAnimation();
            }
            else if (other.GetComponentInParent<AbstractTileContainer>() != null)
            {
                tileContainer = other.GetComponentInParent<AbstractTileContainer>();
                Direction = tileContainer.TileModel.GetDirection(Position,Direction, other, this).normalized;
                tileContainer.TileModel.InteractByBall(this, other);
                tileContainer.StartGlowUpTileAnimation();
            }
            else
            {
                Vector3 normal = (other.ClosestPoint(_container.Transform.position) - _container.Transform.position).normalized;
                Direction = Vector3.Reflect(Direction, normal).normalized;
            }

            _currentBounce++;
            
        }
    }
}