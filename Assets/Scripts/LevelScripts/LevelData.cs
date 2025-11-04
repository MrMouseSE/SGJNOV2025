using System.Collections.Generic;
using System.Linq;
using TileObjectScripts;
using UnityEngine;

namespace LevelScripts
{
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public int LevelDifficulty;
        public int ClearSightCount;
        public int ButtonsCount;
        public int WallHitCount;
        public List<TileObjectHandler> LevelTilesHandlers;

        public void SaveLevelHandlerToLevelData(LevelHandler levelHandler)
        {
            LevelName = levelHandler.LevelName;
            LevelDifficulty = levelHandler.LevelDifficulty;
            LevelTilesHandlers = levelHandler.LevelTilesObjectHandler;
            ClearSightCount = LevelTilesHandlers.Count(handler => handler.TileType == TilesTypes.ClearSight);
            ButtonsCount = LevelTilesHandlers.Count(handler => handler.TileType == TilesTypes.Button);
            WallHitCount = levelHandler.WallHitCount;
        }
    }
}