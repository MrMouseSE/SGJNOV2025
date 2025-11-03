using LevelScripts;

namespace ClearSightScripts
{
    public class ClearSightSystem : IGameSystem
    {
        public ClearSightModel Model;

        public ClearSightSystem(LevelDescription levelDescription, int currentDifficulty)
        {
            Model = new ClearSightModel(levelDescription, currentDifficulty);
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