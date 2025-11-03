using UnityEngine;

namespace BallScripts
{
    public class BallFactory
    {
        private BallContainer _ballContainer;
        
        public BallFactory(BallContainer ballContainer)
        {
            _ballContainer = ballContainer;
        }

        public BallSystem CreateBall(Transform parent)
        {
            BallContainer newBall = GameObject.Instantiate(_ballContainer);
            
            newBall.Transform.SetParent(parent);
            newBall.Transform.localPosition = Vector3.zero;
            
            return new BallSystem(newBall);
        }
    }
}