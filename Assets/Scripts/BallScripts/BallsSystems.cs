namespace BallScripts
{
    public class BallsSystems : IGameSystem
    {
        public BallsSystemsModel Model;

        private bool _isInited;

        public BallsSystems()
        {
            _isInited = true;
            Model = new BallsSystemsModel();
        }
        
        public void InitGameSystem()
        {
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            if (!_isInited) return;
            Model.UpdateModel(deltaTime, gameContext);
        }
    }
}