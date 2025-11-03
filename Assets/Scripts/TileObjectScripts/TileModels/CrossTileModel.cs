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
        
        private bool _isCanBeMoved;

        public bool CheckMoveAvailability()
        {
            return _isCanBeMoved;
        }

        public void SetMoveByPlayerAvailability(bool isCanBeMoved)
        {
            _isCanBeMoved = isCanBeMoved;
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
        }

        public Vector3 GetDirection(BallModel ballModel, Collider touchedCollider)
        {
            DestroyBall(ballModel.Direction, ballModel.Position);
            _gameContext.DestroyBall(ballModel);
            return ballModel.Direction;
        }

        private void DestroyBall(Vector3 ballDirection, Vector3 ballPosition)
        {
            _container.DestroyBallParticlesTransform.position = ballPosition;
            _container.DestroyBallParticlesTransform.forward = ballDirection;
            _container.DestroyBallParticles.Play();
        }
    }
}