using System.Collections.Generic;
using LevelScripts;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace GameSceneScripts.TilesGeneratorScripts
{
    public class TilesGeneratorSystem : IGameSystem
    {
        public LevelDescription LevelDescription;
        public TilesDescription TilesDescription;
        public TilesGeneratorModel Model;

        public TilesGeneratorSystem(LevelDescription levelDescription, TilesDescription tilesDescription)
        {
            TilesDescription = tilesDescription;
            LevelDescription = levelDescription;
            Model = new TilesGeneratorModel();
            
        }
        
        public void InitGameSystem()
        {
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            GameSceneHandler sceneHandler = (GameSceneHandler)gameContext.SceneHandler.GetSceneHandlerByName(gameContext.SceneHandler.GameSceneName);
            if (sceneHandler == null) return;
            if (!gameContext.RegenerateLevel) return;
            gameContext.TileObjectHandlers = GenerateTiles(gameContext.CurrentDifficulty, sceneHandler.TilesHolder);
            gameContext.RegenerateLevel = false;
        }
        
        private List<TileObjectHandler> GenerateTiles(int currentDifficulty, Transform parentTransform)
        {
            return Model.GenerateTiles(LevelDescription, TilesDescription, currentDifficulty, parentTransform);
        }
    }
}