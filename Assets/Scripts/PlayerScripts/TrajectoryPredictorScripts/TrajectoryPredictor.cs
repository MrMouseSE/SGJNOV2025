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
            if (_lineRenderer == null || ballModel == null)
                return;
            
            _mousePoint = direction.magnitude;
            
            direction.Normalize();

            float dot = Vector3.Dot(playerTransform.forward, direction);
            
            if (dot < _minForwardDot)
            {
                direction = Vector3.Reflect(direction, -playerTransform.forward);
            }

            Vector3 currentPosition = startPosition;
            Vector3 currentDirection = direction;
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, startPosition);

            int totalPoints = 1;

            for (int i = 0; i < ballModel.Bounces; i++)
            {
                if (Physics.Raycast(currentPosition, currentDirection, out RaycastHit hit, _maxLength))
                {
                    totalPoints++;
                    _lineRenderer.positionCount = totalPoints;
                    _lineRenderer.SetPosition(totalPoints - 1, hit.point);

                    if (hit.collider.TryGetComponent(out AbstractTileContainer tileModel))
                    {
                        Debug.Log(tileModel);
                        currentDirection = tileModel.TileModel.GetDirection(ballModel, hit.collider).normalized;
                    }
                    else
                    {
                        currentDirection = Vector3.Reflect(currentDirection, hit.normal).normalized;
                    }

                    currentPosition = hit.point + currentDirection * 0.01f;
                }
                else
                {
                    float distanceToCursor = Vector3.Distance(startPosition, startPosition + direction * _mousePoint);
                    totalPoints++;
                    _lineRenderer.positionCount = totalPoints;
                    _lineRenderer.SetPosition(totalPoints - 1, currentPosition + currentDirection * distanceToCursor);
                    break;
                }
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