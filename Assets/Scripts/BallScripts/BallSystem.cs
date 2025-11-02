
using UnityEngine;

namespace BallScripts
{
    public class BallSystem : IGameSystem
    {
        private readonly BallModel _model;
        private readonly BallContainer _container;

        public BallSystem(BallContainer container)
        {
            _model = new BallModel(container);
            _container = container;
        }

        public void InitGameSystem() { }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            _model.Move(deltaTime);
        }

        public void SetDirection(Vector3  direction)
        {
            _container.Direction = direction.normalized;
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