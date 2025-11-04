using System;
using BallScripts;
using MovableTileScripts;
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

        private BallSystem _currentBallSystem;
        private Camera _camera;
        
        public PlayerModel(PlayerContainer container, GameContext gameContext)
        {
            _container = container;
            _inputSystem = gameContext.InputSystem;
            
            _inputSystem.Player.Attack.started += Shoot;
            _inputSystem.Player.Jump.started += SpawnBall;
            
            _gameContext = gameContext;

            PlayerMover = new PlayerMover(gameContext.InputSystem, container.Transform, container.MovementSpeed, gameContext.LeftEndPoint.x);
        }

        public void Dispose()
        {
            _inputSystem.Player.Attack.started -= Shoot;
            _inputSystem.Player.Jump.started -= SpawnBall;
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

        private void SpawnBall(InputAction.CallbackContext obj)
        {
            if (_camera == null)
                _camera = _gameContext.SceneHandler.GetSceneHandlerByName("GameScene").SceneCamera;
            
            var ballsSystem = (BallsSystems)_gameContext.GetGameSystemByType(typeof(BallsSystems));
            ballsSystem.Model.DestroyAllBalls();
            _currentBallSystem = _gameContext.BallFactory.CreateBall(_container.BallHoldPoint);
        }

        private void Shoot(InputAction.CallbackContext obj)
        {
            if (_currentBallSystem == null)
                return;
            if (((MovableTileSystem)_gameContext.GetGameSystemByType(typeof(MovableTileSystem))).Model.IsObjectMovedRightNow()) return;
    
            Vector2 mouseScreenPosition = Input.mousePosition;
    
            Vector3 realMouseWorldPosition = ScreenToWorldPosition(mouseScreenPosition);
    
            Vector3 limitedMousePosition = realMouseWorldPosition;
            limitedMousePosition.z = Mathf.Max(limitedMousePosition.z, 1f);
    
            Vector3 direction = (realMouseWorldPosition - _container.BallHoldPoint.position);
            direction.y = 0;
            direction = direction.normalized;
    
            _currentBallSystem.SetDirection(direction);
            _currentBallSystem.SetVelocity(_container.BallSpeed);
    
            var ballsSystems = (BallsSystems)_gameContext.GetGameSystemByType(typeof(BallsSystems));
            ballsSystems.Model.AddBallSystem(_currentBallSystem);
            
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