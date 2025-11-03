using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class DestroeybleTileModel : DefaultTileModel
    {
        private readonly AbstractTileContainer _container;

        private int _hitCount;
        
        private bool _isAnimating;
        private float _curentAnimationTime;
        private bool _isAnimatingFinished;
        
        public DestroeybleTileModel(AbstractTileContainer container, GameContext gameContext) : base(container)
        {
            _container = container;
            _hitCount = gameContext.GetCurrentLevelData().WallHitCount;
        }

        public override void UpdateModel(float deltaTime, GameContext gameContext)
        {
            if (!_isAnimating || _isAnimatingFinished) return;
            foreach (var collider in _container.Colliders)
            {
                collider.enabled = false;
            }
            _curentAnimationTime += deltaTime;
            float positionValue = 1f;
            if (_curentAnimationTime < _container.UniversalAnimationCurve.keys[^1].time)
            {
                positionValue = _container.UniversalAnimationCurve.Evaluate(_curentAnimationTime);
            }
            else
            {
                _isAnimatingFinished = true;
            }
            _container.AnimationRootTransform.position = _container.AnimationDirection * positionValue;
        }

        public override Vector3 GetDirection(BallContainer ball, Collider touchingCollider)
        {
            _hitCount--;
            if (_hitCount == 0)
            {
                _isAnimating = true;
            }
            return base.GetDirection(ball, touchingCollider);
        }
    }
}