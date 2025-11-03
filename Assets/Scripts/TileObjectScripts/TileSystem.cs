using TileObjectScripts.TileContainers;

namespace TileObjectScripts
{
    public class TileSystem : IGameSystem
    {
        private AbstractTileContainer _container;
        private ITileModel _model;

        public TileSystem(AbstractTileContainer container)
        {
            _container = container;
            _model = TileModelFactory.Create(container);
            _container.Initialize(_model);
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