using BallScripts;
using UnityEngine;

namespace TileObjectScripts
{
    public interface ITileModel
    {
        public Vector3 GetDirection(BallContainer ball, Collider touchedCollider);
    }
}