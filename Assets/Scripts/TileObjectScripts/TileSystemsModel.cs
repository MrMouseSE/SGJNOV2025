using System.Collections.Generic;

namespace TileObjectScripts
{
    public class TileSystemsModel
    {
        private List<TileSystem> _staticTilesSystems = new List<TileSystem>();
        private bool _isStaticInitialized;
        public bool IsDinamycInitialized;
        private List<TileSystem> _tileSystems = new List<TileSystem>();

        public void InitializeModel(GameContext gameContext)
        {
            if (!_isStaticInitialized)
            {
                _isStaticInitialized = true;
                foreach (var handler in gameContext.StaticTileObjectHandlers)
                {
                    _staticTilesSystems.Add(new TileSystem(handler, gameContext));
                }
            }
            if(IsDinamycInitialized) return;
            IsDinamycInitialized = true;
            _tileSystems.Clear();
            foreach (var handler in gameContext.TileObjectHandlers)
            {
                _tileSystems.Add(new TileSystem(handler, gameContext));
            }
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
            foreach (var tileSystem in _tileSystems)
            {
                tileSystem.UpdateGameSystem(deltaTime, gameContext);
            }
        }
    }
}