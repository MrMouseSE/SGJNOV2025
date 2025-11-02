using System.Collections.Generic;
using UnityEngine;

namespace LevelScripts
{
    [PreferBinarySerialization]
    [CreateAssetMenu(menuName = "Create LevelDescription", fileName = "LevelDescription", order = 0)]
    public class LevelDescription : ScriptableObject
    {
        public List<LevelData> LevelData;
    }
}