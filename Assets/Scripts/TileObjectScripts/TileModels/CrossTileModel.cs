using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class CrossTileModel : DefaultTileModel
    {
        private readonly CrossTileContainer _container;
        private GameContext _gameContext;
        
        public CrossTileModel(CrossTileContainer container, GameContext gameContext) : base(container)
        {
            _container = container;
            _gameContext = gameContext;
        }
        
        public override void UpdateModel(float deltaTime, GameContext gameContext)
        {
        }

        public override void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
            ballModel.DestroyBall();
        }

        public override Vector3 GetDirection(BallModel ballModel, Collider touchedCollider)
        {
            return ballModel.Direction;
        }

        private void DestroyBallEffectPlay(Vector3 ballDirection, Vector3 ballPosition)
        {
            _container.DestroyBallParticlesTransform.position = ballPosition;
            _container.DestroyBallParticlesTransform.forward = ballDirection;
            _container.DestroyBallParticles.Play();
        }
    }
}