using System.Collections.Generic;
using GameSceneScripts.TileObjectScripts;
using UnityEngine;

namespace LevelScripts
{
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public int LevelDifficulty;
        
        [HideInInspector]
        public List<TileObjectHandler> LevelTilesHandlers;

        public void SaveLevelHandlerToLevelData(LevelHandler levelHandler)
        {
            LevelName = levelHandler.LevelName;
            LevelDifficulty = levelHandler.LevelDifficulty;
            LevelTilesHandlers = levelHandler.LevelTiles;
        }
    }
}