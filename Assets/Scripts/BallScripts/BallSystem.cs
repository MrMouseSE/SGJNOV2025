
using UnityEngine;

namespace BallScripts
{
    public class BallSystem : IGameSystem
    {
        public BallContainer Container;
        public BallModel Model;
        
        private GameContext _gameContext;

        public BallSystem(BallContainer container, GameContext gameContext)
        {
            Model = new BallModel(this, container, gameContext);
            Container = container;
            _gameContext = gameContext;
        }

        public void InitGameSystem() { }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            if (gameContext.IsGamePaused) return;
            Model.Move(deltaTime);
        }

        public void SetDirection(Vector3 direction)
        {
            Model.SetDirection(direction);
        }

        public void SetVelocity(float velocity)
        {
            Model.Velocity = velocity;
        }

        public void Dispose()
        {
            Model.Dispose();
        }
    }
}