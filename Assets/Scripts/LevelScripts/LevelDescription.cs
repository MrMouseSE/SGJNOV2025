using System.Collections.Generic;
using UnityEngine;

namespace LevelScripts
{
    [CreateAssetMenu(menuName = "Create LevelDescription", fileName = "LevelDescription", order = 0)]
    public class LevelDescription : ScriptableObject
    {
        public Vector4 LevelPlayebleArea;

        [Space]
        public Texture2D NormalCursor;
        public Texture2D HandCursor;
        public Texture2D FistCursor;
        
        [Space]
        public float HandleImpactRadius;
        public Vector3 TilesHandledOffset;
        public AnimationCurve HandleDirectAnimationCurve;
        public AnimationCurve HandleArcAnimationCurve;
        
        [Space]
        public float ToDartAnimationTime;
        public float BeforDartAnimationDelay;
        public float FinalAnimationNextTileDelay;
        [ColorUsage(true, true)] public Color AvailableToMoveColor;
        [ColorUsage(true, true)] public Color NotAvailableToMoveColor;
        public List<LevelData> LevelData;
        
    }
}