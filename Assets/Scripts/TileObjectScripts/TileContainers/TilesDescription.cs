using System.Collections.Generic;
using UnityEngine;

namespace TileObjectScripts.TileContainers
{
    [CreateAssetMenu(menuName = "Create TilesDescription", fileName = "TilesDescription", order = 0)]
    public class TilesDescription : ScriptableObject
    {
        public List<AbstractTileContainer> TileContainers;
    }
}
