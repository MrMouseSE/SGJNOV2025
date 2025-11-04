using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class ClearSightTileModel : DefaultTileModel
    {
        private readonly ClearSightTileContainer _container;
        
        private GameContext _gameContext;

        public ClearSightTileModel(ClearSightTileContainer container, GameContext gameContext) : base(container)
        {
            _gameContext = gameContext;
            _container = container;
            _container.ClearSightTileAnimation.Play(_container.IdleAnimationName);
        }
        
        private bool _isClearSightLooted;

        public override void UpdateModel(float deltaTime, GameContext gameContext)
        {
        }

        public override void InteractByBall(BallModel ballModel, Collider touchedCollider)
        {
            if (_isClearSightLooted) return;
            StartLootAnimation();
            foreach (var collider in _container.Colliders)
            {
                collider.enabled = false;
            }
        }

        public override Vector3 GetDirection(Vector3 direction, Vector3 position, Collider touchedCollider, BallModel ballModel)
        {
            return direction;
        }
        
        private void StartLootAnimation()
        {
            _gameContext.ClearSightLootedCount++;
            _isClearSightLooted = true;
            _container.ClearSightTileAnimation.Play(_container.LootAnimationName);
        }
    }
}