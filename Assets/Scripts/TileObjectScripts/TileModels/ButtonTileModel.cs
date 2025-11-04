using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class ButtonTileModel : DefaultTileModel
    {
        private AbstractTileContainer _container;

        public ButtonTileModel(AbstractTileContainer container) : base(container)
        {
            _container = container;
        }
        
        private bool _isAnimating;
        private float _curentAnimationTime;
        private bool _isAnimatingFinished;
        private Color _startGlowColor;
        private Color _endGlowColor;
        
        

        public override void UpdateModel(float deltaTime, GameContext gameContext)
        {
            if (!_isAnimating || _isAnimatingFinished) return;
            _curentAnimationTime += deltaTime;
            if (_curentAnimationTime < _container.UniversalAnimationCurve.keys[^1].time)
            {
                float value = _container.UniversalAnimationCurve.Evaluate(_curentAnimationTime);
                Color currentColor = Color.Lerp(_startGlowColor, _endGlowColor, value);
                _container.SpecialMeshRenderer.material.SetColor(_container.EmissionColor, currentColor);
            }
            else
            {
                _isAnimatingFinished = true;
                gameContext.ButtonPressed();
            }
        }

        public override void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
            if (_isAnimatingFinished || _isAnimating) return;
            _isAnimating = true;
            _startGlowColor = _container.GlowMeshRenderer.material.GetColor(_container.EmissionColor);
            _endGlowColor = Color.green;
        }

        public override Vector3 GetDirection(Vector3 direction, Vector3 position, Collider touchedCollider, BallModel ballModel)
        {
            return direction;
        }
    }
}