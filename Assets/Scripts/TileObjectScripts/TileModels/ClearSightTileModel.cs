using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class ClearSightTileModel : ITileModel
    {
        private readonly ClearSightTileContainer _container;

        public ClearSightTileModel(ClearSightTileContainer container)
        {
            _container = container;
            _container.ClearSightTileAnimation.Play(_container.IdleAnimationName);
        }
        
        private bool _isClearSightLooted;
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
            if (!_isClearSightLooted) return;
            _isClearSightLooted = false;
            gameContext.ClearSightLootedCount++;
        }

        public Vector3 GetDirection(BallContainer ball, Collider touchedCollider)
        {
            StartLootAnimation();
            foreach (var collider in _container.Colliders)
            {
                collider.enabled = false;
            }
            return ball.Direction;
        }
        
        private void StartLootAnimation()
        {
            _isClearSightLooted = true;
            _container.ClearSightTileAnimation.Play(_container.LootAnimationName);
        }
    }
}