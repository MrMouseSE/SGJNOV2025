using TileObjectScripts.TileContainers;

namespace TileObjectScripts.TileModels
{
    public class DoorTileModel : DefaultTileModel
    {
        private GameContext _gameContext;
        private readonly AbstractTileContainer _container;

        private bool _isAnimating;
        private float _curentAnimationTime;
        private bool _isAnimatingFinished;

        public DoorTileModel(AbstractTileContainer container, GameContext gameContext) : base(container)
        {
            _container = container;
            _gameContext = gameContext;
            _gameContext.AllButtonsPressed += () => _isAnimating = true;
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
    }
}