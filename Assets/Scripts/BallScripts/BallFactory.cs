using UnityEngine;

namespace BallScripts
{
    public class BallFactory
    {
        private GameContext _gameContext;
        private BallContainer _ballContainer;
        
        public BallFactory(BallContainer ballContainer, GameContext gameContext)
        {
            _gameContext = gameContext;
            _ballContainer = ballContainer;
        }

        public BallSystem CreateBall(Transform parent)
        {
            BallContainer newBall = Object.Instantiate(_ballContainer, parent, true);

            newBall.Transform.localPosition = Vector3.zero;
            
            return new BallSystem(newBall, _gameContext);
        }
    }
}