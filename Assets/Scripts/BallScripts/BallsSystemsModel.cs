using System.Collections.Generic;
using UnityEngine;

namespace BallScripts
{
    public class BallsSystemsModel
    {
        public List<BallSystem> BallSystems = new();

        public void AddBallSystem(BallSystem system)
        {
            BallSystems.Add(system);
        }

        public void RemoveBallSystem(BallSystem system)
        {
            BallSystems.Remove(system);
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
            foreach (var ballSystem in BallSystems)
            {
                ballSystem.UpdateGameSystem(deltaTime, gameContext);
            }
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