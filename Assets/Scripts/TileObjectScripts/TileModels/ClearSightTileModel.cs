using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class ClearSightTileModel : DefaultTileModel
    {
        private readonly ClearSightTileContainer _container;

        public ClearSightTileModel(ClearSightTileContainer container) : base(container)
        {
            _container = container;
            _container.ClearSightTileAnimation.Play(_container.IdleAnimationName);
        }
        
        private bool _isClearSightLooted;

        public override void UpdateModel(float deltaTime, GameContext gameContext)
        {
            if (!_isClearSightLooted) return;
            _isClearSightLooted = false;
            gameContext.ClearSightLootedCount++;
        }

        public override void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
            StartLootAnimation();
            foreach (var collider in _container.Colliders)
            {
                collider.enabled = false;
            }
        }

        public Vector3 GetDirection(Vector3 direction, Vector3 position, Collider touchedCollider)
        {
            return direction;
        }
        
        private void StartLootAnimation()
        {
            _isClearSightLooted = true;
            _container.ClearSightTileAnimation.Play(_container.LootAnimationName);
        }
    }
}