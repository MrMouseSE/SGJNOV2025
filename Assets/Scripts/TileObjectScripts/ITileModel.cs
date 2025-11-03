using BallScripts;
using UnityEngine;

namespace TileObjectScripts
{
    public interface ITileModel
    {
        public Vector3 GetDirection(BallModel ballModel, Collider touchedCollider);
    }
}