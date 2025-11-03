using BallScripts;
using UnityEngine;

namespace TileObjectScripts
{
    public interface ITileModel
    {
        public void UpdateModel(float deltaTime, GameContext gameContext);
        public Vector3 GetDirection(BallContainer ball, Collider touchedCollider);
    }
}