using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class CrossTileModel : ITileModel
    {
        private readonly CrossTileContainer _container;
        private GameContext _gameContext;

        public CrossTileModel(CrossTileContainer container, GameContext gameContext)
        {
            _container = container;
            _gameContext = gameContext;
        }
        public Vector3 GetDirection(BallContainer ball, Collider touchedCollider)
        {
            DestroyBall(ball.Direction, ball.Position);
            _gameContext.DestroyBall(ball);
            return ball.Direction;
        }

        private void DestroyBall(Vector3 ballDirection, Vector3 ballPosition)
        {
            _container.DestroyBallParticlesTransform.position = ballPosition;
            _container.DestroyBallParticlesTransform.forward = ballDirection;
            _container.DestroyBallParticles.Play();
        }
    }
}