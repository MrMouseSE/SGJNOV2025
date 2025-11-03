using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class CrossTileModel : ITileModel
    {
        private readonly AbstractTileContainer _container;

        public CrossTileModel(CrossTileContainer container)
        {
            _container = container;
        }
        public Vector3 GetDirection(BallContainer ball, Collider touchedCollider)
        {
            
        }

        private void DestroyBall()
        {
            
        }
    }
}