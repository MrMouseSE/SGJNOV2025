namespace BallScripts
{
    public class BallSystem : IGameSystem
    {
        private readonly BallModel _model;

        public BallSystem(BallContainer container)
        {
            _model = new BallModel(container);
        }

        public void InitGameSystem() { }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            if (gameContext.IsGamePaused) return;
            _model.Move(deltaTime);
        }

        public void Dispose()
        {
            _model.Dispose();
        }
    }
}