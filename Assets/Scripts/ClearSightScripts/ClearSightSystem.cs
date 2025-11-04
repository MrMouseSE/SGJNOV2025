using LevelScripts;

namespace ClearSightScripts
{
    public class ClearSightSystem : IGameSystem
    {
        public ClearSightModel Model;

        public ClearSightSystem(LevelDescription levelDescription)
        {
            Model = new ClearSightModel(levelDescription);
        }

        public void InitGameSystem()
        {
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            Model.UpdateModel(deltaTime, gameContext);
        }
    }
}