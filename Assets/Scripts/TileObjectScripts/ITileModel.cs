using BallScripts;
using UnityEngine;

namespace TileObjectScripts
{
    public interface ITileModel
    {
        public bool CheckMoveAvailability();
        public void SetMoveByPlayerAvailability(bool isCanBeMoved);
        public void UpdateModel(float deltaTime, GameContext gameContext);
        public void InteractByBall(BallModel ballModel, Collider touchedCollider);
        public Vector3 GetDirection(BallModel ballModel, Collider touchedCollider);
        public void StartHandledAnimation(Vector3 destinationPosition, AnimationCurve animationCurve);
        public void SetPrefabPosition(Vector3 position);
    }
}