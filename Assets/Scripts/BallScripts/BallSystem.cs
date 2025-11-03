
using UnityEngine;

namespace BallScripts
{
    public class BallSystem : IGameSystem
    {
        public BallContainer Container;
        
        private readonly BallModel _model;

        public BallSystem(BallContainer container)
        {
            _model = new BallModel(container);
            Container = container;
        }

        public void InitGameSystem() { }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            if (gameContext.IsGamePaused) return;
            _model.Move(deltaTime);
        }

        public void SetDirection(Vector3 direction)
        {
            _model.SetDirection(direction);
        }

        public void SetVelocity(float velocity)
        {
            _model.Velocity = velocity;
        }

        public void Dispose()
        {
            _model.Dispose();
        }
    }
}