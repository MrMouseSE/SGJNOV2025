using TileObjectScripts.TileContainers;

namespace TileObjectScripts
{
    public class TileSystem : IGameSystem
    {
        private AbstractTileContainer _container;
        private ITileModel _model;

        public TileSystem(TileObjectHandler handler)
        {
            _container = handler.TilePrefab;
            _model = TileModelFactory.Create(handler.TilePrefab);
            _container.Initialize(_model, handler.IsTileGlowAtStart);
        }

        public void InitGameSystem()
        {
            
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            if (gameContext.IsGamePaused) return;
        }
    }
}