namespace TileObjectScripts
{
    public class TileSystemsSystem : IGameSystem
    {
        public TileSystemsModel Model;
        private GameContext _gameContext;

        public TileSystemsSystem(GameContext gameContext)
        {
            Model = new TileSystemsModel();
            _gameContext = gameContext;
        }
        
        public void InitGameSystem()
        {
            Model.InitializeModel(_gameContext);
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            Model.UpdateModel(deltaTime, gameContext);
        }
    }
}