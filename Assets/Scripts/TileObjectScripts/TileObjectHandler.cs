using System;
using GameSceneScripts.TileObjectScripts.TileContainers;
using UnityEngine;

namespace GameSceneScripts.TileObjectScripts
{
    [Serializable]
    public class TileObjectHandler
    {
        public Vector3 TilePosition;
        public TilesTypes TileType;
        public AbstractTileContainer AbstractTilePrefab;
    }
}