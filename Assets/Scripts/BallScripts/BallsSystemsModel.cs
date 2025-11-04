using System.Collections.Generic;

namespace BallScripts
{
    public class BallsSystemsModel
    {
        public List<BallSystem> BallSystems = new();
        
        private List<BallSystem> _ballSystemsToRemove = new();
        private List<BallSystem> _ballSystemsToAdd = new();

        public bool IsAnyBallMoving()
        {
            bool anyBallMoving = false;
            foreach (var ballSystem in BallSystems)
            {
                anyBallMoving |= ballSystem.Model.IsBallMoving();
            }
            return anyBallMoving;
        }

        public void AddBallSystem(BallSystem system)
        {
            _ballSystemsToAdd.Add(system);
        }

        public void RemoveBallSystem(BallSystem system)
        {
            _ballSystemsToRemove.Add(system);
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
            foreach (var ballSystem in BallSystems)
            {
                ballSystem.UpdateGameSystem(deltaTime, gameContext);
            }
            
            BallSystems.RemoveAll(ballSystem => _ballSystemsToRemove.Contains(ballSystem));
            foreach (var ballSystem in _ballSystemsToRemove)
            {
                ballSystem.Model.DestroyGameObject();
            }
            _ballSystemsToRemove.Clear();
            BallSystems.AddRange(_ballSystemsToAdd);
            _ballSystemsToAdd.Clear();
        }

        public void DestroyAllBalls()
        {
            for (var index = 0; index < BallSystems.Count; index++)
            {
                var ballSystem = BallSystems[index];
                ballSystem.Model.DestroyBall();
            }
            BallSystems.Clear();
        }
    }
}