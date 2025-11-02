using System.Collections.Generic;
using TileObjectScripts;
using UnityEngine;

namespace LevelScripts
{
    [PreferBinarySerialization]
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public int LevelDifficulty;
        
        [SerializeField]
        public List<TileObjectHandler> LevelTilesHandlers;

        public void SaveLevelHandlerToLevelData(LevelHandler levelHandler)
        {
            LevelName = levelHandler.LevelName;
            LevelDifficulty = levelHandler.LevelDifficulty;
            LevelTilesHandlers = levelHandler.LevelTilesObjectHandler;
        }
    }
}