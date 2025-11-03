using BallScripts;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

namespace PlayerScripts.TrajectoryPredictorScripts
{
    public class TrajectoryPredictor : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _maxLength = 10f;
        [SerializeField] private float _mousePoint;
        [SerializeField] private int _maxPoints = 20;

        private float _minForwardDot = 0f;

        private void Awake()
        {
            _lineRenderer.useWorldSpace = true;
        }

        public void ShowTrajectory(Vector3 startPosition, Vector3 direction, BallModel ballModel,
            Transform playerTransform)
        {
            if (_lineRenderer == null || ballModel == null) return;

            direction.Normalize();
            float sphereRadius = ballModel.Collider.bounds.extents.x;
            float dot = Vector3.Dot(playerTransform.forward, direction);
            if (dot < _minForwardDot)
                direction = Vector3.Reflect(direction, -playerTransform.forward);

            Vector3 currentPosition = startPosition;
            Vector3 currentDirection = direction;

            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, startPosition);
            int totalPoints = 1;

            for (int i = 0; i < ballModel.Bounces; i++)
            {
                if (!Physics.SphereCast(currentPosition, sphereRadius, currentDirection, out RaycastHit hit,
                        _maxLength))
                {
                    totalPoints++;
                    _lineRenderer.positionCount = totalPoints;
                    _lineRenderer.SetPosition(totalPoints - 1, currentPosition + currentDirection * _mousePoint);
                    break;
                }

                Vector3 reflectedDir;
                if (hit.collider.TryGetComponent(out AbstractTileContainer tile))
                {
                    reflectedDir = tile.TileModel.GetDirection(currentDirection, hit.point, hit.collider).normalized;
                }
                else
                {
                    reflectedDir = Vector3.Reflect(currentDirection, hit.normal).normalized;
                }

                totalPoints++;
                _lineRenderer.positionCount = totalPoints;
                _lineRenderer.SetPosition(totalPoints - 1, hit.point - reflectedDir * sphereRadius);

                currentPosition = hit.point + reflectedDir * 0.01f;
                currentDirection = reflectedDir;

                if (tile != null && !tile.IsGlowing)
                    break;
            }

            _lineRenderer.enabled = true;
        }

        public void HideTrajectory()
        {
            if (_lineRenderer != null)
                _lineRenderer.enabled = false;
        }
    }
}