namespace MovableTileScripts
{
    public class MovableTileSystem : IGameSystem
    {
        public MovableTileModel Model;
        
        private GameContext _gameContext;
        private bool _initialized = false;

        public MovableTileSystem(GameContext gameContext)
        {
            Model = new MovableTileModel(gameContext);
            _gameContext = gameContext;
        }
        
        public void InitGameSystem()
        {
            Model.InitModel();
            _initialized = true;
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            if (!_initialized) return;
            Model.UpdateModel();
        }
    }
}