using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class ButtonTileModel : ITileModel
    {
        private AbstractTileContainer _container;

        public ButtonTileModel(AbstractTileContainer container)
        {
            _container = container;
        }
        
        private bool _isAnimating;
        private float _curentAnimationTime;
        private bool _isAnimatingFinished;
        private Color _startGlowColor;
        private Color _endGlowColor;

        private bool _isCanBeMoved;

        public bool CheckMoveAvailability()
        {
            return _isCanBeMoved;
        }

        public void SetMoveByPlayerAvailability(bool isCanBeMoved)
        {
            _isCanBeMoved = isCanBeMoved;
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
            if (!_isAnimating || _isAnimatingFinished) return;
            _curentAnimationTime += deltaTime;
            if (_curentAnimationTime < _container.UniversalAnimationCurve.keys[^1].time)
            {
                float value = _container.UniversalAnimationCurve.Evaluate(_curentAnimationTime);
                Color currentColor = Color.Lerp(_startGlowColor, _endGlowColor, value);
                _container.GlowMeshRenderer.material.SetColor(_container.EmissionColor, currentColor);
            }
            else
            {
                _isAnimatingFinished = true;
                gameContext.ButtonPressed();
            }
        }

        public Vector3 GetDirection(BallContainer ball, Collider touchedCollider)
        {
            if (_isAnimatingFinished || _isAnimating) return ball.Direction;
            _startGlowColor = _container.GlowMeshRenderer.material.GetColor(_container.EmissionColor);
            _endGlowColor = Color.green;
            return ball.Direction;
        }
    }
}