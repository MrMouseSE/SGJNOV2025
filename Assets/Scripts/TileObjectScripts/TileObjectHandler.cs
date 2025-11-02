using System;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts
{
    [Serializable]
    public class TileObjectHandler
    {
        public Vector3 TilePosition;
        public TilesTypes TileType;
        public AbstractTileContainer AbstractTilePrefab;
    }
}