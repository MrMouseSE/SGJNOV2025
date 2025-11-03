using System.Collections.Generic;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace GameSceneScripts.TilesGeneratorScripts
{
    public static class TilesGeneratorStaticFactory
    {
        public static List<TileObjectHandler> GenerateTilesToWorld(List<TileObjectHandler> levelTilesHandlers, TilesDescription tilesDescription, Transform parentTransform = null)
        {
            List<TileObjectHandler> handlers = new List<TileObjectHandler>();
            foreach (var tileDescriptionHandler in levelTilesHandlers)
            {
                var tileContainer = tilesDescription.TileContainers.Find(x=>x.TileType == tileDescriptionHandler.TileType);
                AbstractTileContainer abstractTile = Object.Instantiate(tileContainer, tileDescriptionHandler.TilePosition, Quaternion.identity, parentTransform);
                var tileEditableHandler = new TileObjectHandler();
                tileEditableHandler.IsAvailableToMoveByPlayer = tileDescriptionHandler.IsAvailableToMoveByPlayer;
                tileEditableHandler.IsTileGlowAtStart = tileDescriptionHandler.IsTileGlowAtStart;
                tileEditableHandler.TileType = tileDescriptionHandler.TileType;
                tileEditableHandler.TilePosition = tileDescriptionHandler.TilePosition;
                tileEditableHandler.TilePrefab = abstractTile;
                handlers.Add(tileEditableHandler);
            }
            return handlers;
        }
    }
}