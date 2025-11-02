using System.Collections.Generic;
using LevelScripts;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace GameSceneScripts.TilesGeneratorScripts
{
    public class TilesGeneratorModel
    {
        public List<TileObjectHandler> GenerateTiles(LevelDescription levelDescription, TilesDescription tilesDescription, int currentDifficulty, Transform tilesHolder)
        {
            List<TileObjectHandler> levelTilesHandlers =
                levelDescription.LevelData.Find(x => x.LevelDifficulty == currentDifficulty).LevelTilesHandlers;
            return TilesGeneratorStaticFactory.GenerateTilesToWorld(levelTilesHandlers, tilesDescription, tilesHolder);
        }

        public void DestroyTiles(GameContext gameContext)
        {
            foreach (var tileObjectHandler in gameContext.TileObjectHandlers)
            {
                Object.Destroy(tileObjectHandler.TilePrefab.TileGameObject);
            }
            gameContext.TileObjectHandlers.Clear();
        }
    }
}