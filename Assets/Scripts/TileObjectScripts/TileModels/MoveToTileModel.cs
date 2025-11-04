using BallScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace TileObjectScripts.TileModels
{
    public class MoveToTileModel : DefaultTileModel
    {
        public MoveToTileModel(AbstractTileContainer container) : base(container)
        {
        }

        public override Vector3 GetDirection(Vector3 direction, Vector3 position, Collider touchingCollider, BallModel ballModel)
        {
            return direction;
        }
    }
}