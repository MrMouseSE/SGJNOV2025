using System.Collections.Generic;
using BallScripts;
using UnityEngine;

namespace TileObjectScripts.TileContainers
{
    [CreateAssetMenu(menuName = "Create TilesDescription", fileName = "TilesDescription", order = 0)]
    public class TilesDescription : ScriptableObject
    {
        public BallContainer BallContainer;
        public List<AbstractTileContainer> TileContainers;
    }
}
