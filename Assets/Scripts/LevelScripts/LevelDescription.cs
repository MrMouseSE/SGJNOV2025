using System.Collections.Generic;
using UnityEngine;

namespace LevelScripts
{
    [CreateAssetMenu(menuName = "Create LevelDescription", fileName = "LevelDescription", order = 0)]
    public class LevelDescription : ScriptableObject
    {
        public float ToDartAnimationTime;
        public float FinalAnimationNextTileDelay;
        [ColorUsage(true, true)] public Color AvailableToMoveColor;
        [ColorUsage(true, true)] public Color NotAvailableToMoveColor;
        public List<LevelData> LevelData;
    }
}