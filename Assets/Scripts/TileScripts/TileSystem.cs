namespace TileScript
{
    public class TileSystem : IGameSystem
    {
        private TileContainer _container;
        private ITileModel _model;

        public TileSystem(TileContainer container)
        {
            _container = container;
            _model = TileModelFactory.Create(container);
            _container.Initialize(_model);
        }

        public void InitGameSystem()
        { }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        { }
    }
}