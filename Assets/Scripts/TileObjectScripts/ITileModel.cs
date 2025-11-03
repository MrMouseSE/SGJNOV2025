using BallScripts;
using UnityEngine;

namespace TileObjectScripts
{
    public interface ITileModel
    {
        public bool CheckMoveAvailability();
        public void SetMoveByPlayerAvailability(bool isCanBeMoved);
        public void UpdateModel(float deltaTime, GameContext gameContext);
        public Vector3 GetDirection(BallModel ballModel, Collider touchedCollider);
    }
}