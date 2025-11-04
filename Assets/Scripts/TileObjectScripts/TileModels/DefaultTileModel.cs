using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts
{
    public class DefaultTileModel : ITileModel
    {
        private readonly AbstractTileContainer _container;

        private bool _isHandleAnimating;
        private Vector3 _tileViewFromAnimation;
        private Vector3 _tileViewToAnimation;
        private AnimationCurve _tileAnimationCurve;
        private float _currentHandleAnimationTime;
        private Vector3 _sourceHandleAnimationPosition;
        private Vector3 _targetHandleAnimationPosition;

        public DefaultTileModel(AbstractTileContainer container)
        {
            _container = container;
        }
        
        private bool _isCanBeMoved;

        public virtual bool CheckMoveAvailability()
        {
            return _isCanBeMoved;
        }

        public virtual void SetMoveByPlayerAvailability(bool isCanBeMoved)
        {
            _isCanBeMoved = isCanBeMoved;
        }

        public virtual void UpdateModel(float deltaTime, GameContext gameContext)
        {
            if(!_isHandleAnimating) return;
            HandleAnimate(deltaTime);
        }

        public virtual void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
        }

        public virtual Vector3 GetDirection(BallModel ballModel, Collider touchingCollider)
        {
            return ReflectBall(ballModel, touchingCollider);
        }

        public virtual void StartHandledAnimation(Vector3 destinationPosition, AnimationCurve animationCurve)
        {
            _targetHandleAnimationPosition = destinationPosition;
            _sourceHandleAnimationPosition = _container.HanldingAnimationTransform.localPosition;
            _isHandleAnimating = true;
            _tileAnimationCurve = animationCurve;
            _currentHandleAnimationTime = 0f;
        }

        public virtual void SetPrefabPosition(Vector3 position)
        {
            _container.TileTransform.position = position;
        }

        private Vector3 ReflectBall(BallModel ballModel, Collider touchingCollider)
        {
            if (!_container.Colliders.Contains(touchingCollider)) return ballModel.Direction;
    
            Vector3 closestPoint = touchingCollider.ClosestPoint(ballModel.Position);
    
            Vector3 collisionNormal = (ballModel.Position - closestPoint).normalized;
    
            if (collisionNormal == Vector3.zero)
                collisionNormal = (ballModel.Position - _container.transform.position).normalized;
    
            return Vector3.Reflect(ballModel.Direction, collisionNormal);
        }
        
        private void HandleAnimate(float deltaTime)
        {
            _currentHandleAnimationTime += deltaTime;
            if (_currentHandleAnimationTime > _tileAnimationCurve.keys[^1].time)
            {
                _currentHandleAnimationTime = _tileAnimationCurve.keys[^1].time;
                _isHandleAnimating = false;
            }
            float evaluateValue = _tileAnimationCurve.Evaluate(_currentHandleAnimationTime);
            _container.HanldingAnimationTransform.localPosition = Vector3.Lerp(_sourceHandleAnimationPosition, _targetHandleAnimationPosition, evaluateValue);
        }
    }
}