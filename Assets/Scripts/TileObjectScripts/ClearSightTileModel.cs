using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts
{
    public class ClearSightTileModel : ITileModel
    {
        private readonly ClearSightTileContainer _container;

        private bool _isClearSightLooted;

        public ClearSightTileModel(ClearSightTileContainer container)
        {
            _container = container;
            _container.ClearSightTileAnimation.Play(_container.IdleAnimationName);
        }
        
        public void UpdateModel(GameContext gameContext)
        {
            if (!_isClearSightLooted) return;
            gameContext.ClearSightLootedCount++;
        }

        public Vector3 GetDirection(BallContainer ball, Collider touchedCollider)
        {
            StartLootAnimation();
            return ball.Direction;
        }
        
        private void StartLootAnimation()
        {
            _isClearSightLooted = true;
            _container.ClearSightTileAnimation.Play(_container.LootAnimationName);
        }
    }
}