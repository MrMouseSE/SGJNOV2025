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
            if (gameContext.IsGamePaused) return;
            GameSceneHandler sceneHandler = (GameSceneHandler)gameContext.SceneHandler.GetSceneHandlerByName(gameContext.SceneHandler.GameSceneName);
            if (sceneHandler == null) return;
            if (!gameContext.RegenerateLevel) return;
            DestroyTiles(gameContext);
            var handlers = GenerateTiles(gameContext.CurrentDifficulty, sceneHandler.TilesHolder);
            gameContext.InitializeTilesSystems(handlers);
            gameContext.RegenerateLevel = false;
        }
        
        private List<TileObjectHandler> GenerateTiles(int currentDifficulty, Transform parentTransform)
        {
            return Model.GenerateTiles(LevelDescription, TilesDescription, currentDifficulty, parentTransform);
        }

        private void DestroyTiles(GameContext gameContext)
        {
            Model.DestroyTiles(gameContext);
        }
    }
}