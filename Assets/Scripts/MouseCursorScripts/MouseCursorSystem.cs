namespace MouseCursorScripts
{
    public class MouseCursorSystem : IGameSystem
    {
        public MouseCursorModel Model;

        public MouseCursorSystem(GameContext gameContext)
        {
            Model = new MouseCursorModel(gameContext);
        }
        
        public void InitGameSystem()
        {
            Model.InitModel();
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            Model.UpdateModel(deltaTime, gameContext);
        }
    }
}