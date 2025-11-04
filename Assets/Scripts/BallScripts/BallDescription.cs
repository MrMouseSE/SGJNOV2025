using UnityEngine;

namespace BallScripts
{
    [CreateAssetMenu(menuName = "Create BallDescription", fileName = "BallDescription", order = 0)]
    public class BallDescription : ScriptableObject
    {
        public int Rebounds;
    }
}