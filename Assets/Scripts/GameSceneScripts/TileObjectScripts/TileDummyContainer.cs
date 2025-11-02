using UnityEngine;

namespace GameSceneScripts.TileObjectScripts
{
    public class TileDummyContainer : MonoBehaviour
    {
        public GameObject TileGameObject;
        public Transform TileTransform;
        [HideInInspector]
        public TileObjectHandler TileObjectHandler;
    }
}