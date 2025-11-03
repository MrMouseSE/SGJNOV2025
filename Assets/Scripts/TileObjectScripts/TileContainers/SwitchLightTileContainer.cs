using UnityEngine;

namespace TileObjectScripts.TileContainers
{
    public class SwitchLightTileContainer : AbstractTileContainer
    {
        [Space]
        [ColorUsage(true, true)] public Color InitinGlowLightColor;
        [ColorUsage(true, true)] public Color SwitchToGlowLightColor;
    }
}
