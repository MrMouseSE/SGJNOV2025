using BallScripts;
using UnityEngine;

namespace TileScript
{
    public interface ITileModel
    {
        public Vector3 GetDirection(BallContainer ball);
    }
}