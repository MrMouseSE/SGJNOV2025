using System.Collections.Generic;
using TileObjectScripts;

namespace LevelScripts
{
    public class LevelHandler
    {
        public string LevelName;
        public int LevelDifficulty;
        public int WallHitCount;
        public List<TileObjectHandler> LevelTilesObjectHandler;
    }
}