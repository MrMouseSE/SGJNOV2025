using System.Collections.Generic;
using UnityEngine;

namespace TileObjectScripts.TileContainers
{
    public class AbstractTileContainer : MonoBehaviour
    {
        public GameObject TileGameObject;
        public Transform TileTransform;
        public TilesTypes TileType;
        public MeshRenderer TileMeshRenderer;
        public MeshRenderer GlowMeshRenderer;
        public MeshRenderer AdditionalMeshRenderer;
        
        [Space]
        public List<Collider> Colliders;
        
        [HideInInspector]
        public ITileModel TileModel;
        [HideInInspector]
        public TileObjectHandler TileObjectHandler;
        
        public void Initialize(ITileModel tileModel)
        {
            TileModel = tileModel;
        }
    }
}