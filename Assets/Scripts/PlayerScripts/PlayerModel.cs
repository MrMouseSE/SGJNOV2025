using System;
using BallScripts;
using PlayerScripts.MoverLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerModel : IDisposable
    {
        public PlayerMover PlayerMover;

        private readonly PlayerContainer _container;
        private readonly GameContext _gameContext;
        private readonly InputSystemActions _inputSystem;
        private readonly BallsSystems _ballsSystem;
        private readonly Camera _camera;

        private BallSystem _currentBallSystem;

        public PlayerModel(PlayerContainer container, GameContext gameContext)
        {
            _container = container;
            _inputSystem = gameContext.InputSystem;

            _gameContext = gameContext;
            
            _inputSystem.Player.Attack.started += Shoot;
            _inputSystem.Player.Jump.started += HandleSpawnButton;

            PlayerMover = new PlayerMover(gameContext.InputSystem, container.Transform, container.MovementSpeed,
                gameContext.LeftEndPoint.x);
            
            _ballsSystem = (BallsSystems)_gameContext.GetGameSystemByType(typeof(BallsSystems));
            _ballsSystem.Model.E_AllBallsDestroyed += SpawnBall;
            
            _camera = _gameContext.SceneHandler.GetSceneHandlerByName("GameScene").SceneCamera;
        }

        public void Dispose()
        {
            _inputSystem.Player.Attack.started -= Shoot;
            _inputSystem.Player.Jump.started -= HandleSpawnButton;
            _ballsSystem.Model.E_AllBallsDestroyed -= SpawnBall;
            
            PlayerMover.Dispose();
        }

        public void ShowTrajectory()
        {
            if (_currentBallSystem == null)
                return;

            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPosition = ScreenToWorldPosition(mouseScreenPosition);

            Vector3 direction = (mouseWorldPosition - _container.BallHoldPoint.position);

            _container.TrajectoryPredictor.ShowTrajectory(
                _container.BallHoldPoint.position,
                direction,
                _currentBallSystem.Model,
                _container.Transform
            );
        }

        private void SpawnBall()
        {
            if (_currentBallSystem != null)
                return;
            
            _ballsSystem.Model.DestroyAllBalls();
            _currentBallSystem = _gameContext.BallFactory.CreateBall(_container.BallHoldPoint);
            _ballsSystem.Model.AddBallSystem(_currentBallSystem);
        }

        private void HandleSpawnButton(InputAction.CallbackContext obj)
        {
            SpawnBall();
        }

        private void Shoot(InputAction.CallbackContext obj)
        {
            if (_currentBallSystem == null)
                return;

            Vector2 mouseScreenPosition = Input.mousePosition;

            Vector3 realMouseWorldPosition = ScreenToWorldPosition(mouseScreenPosition);

            Vector3 limitedMousePosition = realMouseWorldPosition;
            limitedMousePosition.z = Mathf.Max(limitedMousePosition.z, 1f);

            Vector3 direction = (realMouseWorldPosition - _container.BallHoldPoint.position);
            direction.y = 0;
            direction = direction.normalized;

            _currentBallSystem.SetDirection(direction);
            _currentBallSystem.SetVelocity(_container.BallSpeed);

            _currentBallSystem.Container.Transform.SetParent(null);
            _currentBallSystem = null;

            _container.TrajectoryPredictor.HideTrajectory();
        }

        private Vector3 ScreenToWorldPosition(Vector2 screenPosition)
        {
            if (_camera != null)
            {
                Ray ray = _camera.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y, 0));
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

                if (groundPlane.Raycast(ray, out float distance))
                {
                    return ray.GetPoint(distance);
                }
            }

            return Vector3.zero;
        }
    }
}