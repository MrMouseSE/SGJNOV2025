using System.Collections.Generic;
using UnityEngine;

namespace BallScripts
{
    public class BallsSystemsModel
    {
        public List<BallSystem> BallSystems = new();
        
        private List<BallSystem> _ballSystemsToRemove = new();

        public void AddBallSystem(BallSystem system)
        {
            BallSystems.Add(system);
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