using System.Collections.Generic;
using LevelScripts;
using MovableTileScripts;
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
        public GameContext GameContext;

        public TilesGeneratorSystem(LevelDescription levelDescription, TilesDescription tilesDescription, GameContext gameContext)
        {
            TilesDescription = tilesDescription;
            LevelDescription = levelDescription;
            Model = new TilesGeneratorModel();
            GameContext = gameContext;
        }
        
        public void InitGameSystem()
        {
            List<TileObjectHandler> staticTileObjectHandlers = new List<TileObjectHandler>();
            GameSceneHandler sceneHandler = (GameSceneHandler)GameContext.SceneHandler.GetSceneHandlerByName(GameContext.SceneHandler.GameSceneName);
            foreach (var staticTilesContainer in sceneHandler.StaticTilesHandler.StaticTilesContainers)
            {
                TileObjectHandler tileObjectHandler = new TileObjectHandler();
                tileObjectHandler.TilePrefab = staticTilesContainer;
                tileObjectHandler.TilePosition = staticTilesContainer.TileTransform.position;
                tileObjectHandler.IsTileGlowAtStart = true;
                tileObjectHandler.IsAvailableToMoveByPlayer = false;
                staticTileObjectHandlers.Add(tileObjectHandler); 
            }

            GameContext.StaticTileObjectHandlers = staticTileObjectHandlers;
            GameContext.InitializeSystemByType(typeof(TileSystemsSystem));
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            if (gameContext.IsGamePaused) return;
            GameSceneHandler sceneHandler = (GameSceneHandler)gameContext.SceneHandler.GetSceneHandlerByName(gameContext.SceneHandler.GameSceneName);
            if (sceneHandler == null) return;
            if (!gameContext.RegenerateLevel) return;
            DestroyTiles(gameContext);
            var handlers = GenerateTiles(gameContext.CurrentDifficulty, sceneHandler.TilesHolder);
            gameContext.TileObjectHandlers = handlers;
            var tileSystem = (TileSystemsSystem)gameContext.GetGameSystemByType(typeof(TileSystemsSystem));
            tileSystem.Model.IsDinamycInitialized = false;
            gameContext.InitializeSystemByType(typeof(TileSystemsSystem));
            gameContext.RegenerateLevel = false;
            gameContext.InitializeSystemByType(typeof(MovableTileSystem));
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