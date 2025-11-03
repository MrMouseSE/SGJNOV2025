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

        private BallSystem _currentBallSystem;
        
        public PlayerModel(InputSystemActions inputSystem, PlayerContainer container, GameContext gameContext)
        {
            _container = container;
            _inputSystem =  inputSystem;
            
            _inputSystem.Player.Attack.started += Shoot;
            _inputSystem.Player.Jump.started += SpawnBall;
            
            _gameContext = gameContext;

            PlayerMover = new PlayerMover(inputSystem, container.Transform, container.MovementSpeed, gameContext.LeftEndPoint.position.x);
        }

        public void Dispose()
        {
            _inputSystem.Player.Attack.started -= Shoot;
            _inputSystem.Player.Jump.started -= SpawnBall;
            PlayerMover.Dispose();
        }
        
        private void SpawnBall(InputAction.CallbackContext obj)
        {
            _currentBallSystem = _gameContext.BallFactory.CreateBall(_container.BallHoldPoint);
        }

        private void Shoot(InputAction.CallbackContext obj)
        {
            if (_currentBallSystem == null)
                return;

            Vector2 mouseScreenPosition = _inputSystem.Player.Look.ReadValue<Vector2>();
    
            Vector3 mouseWorldPosition = ScreenToWorldPosition(mouseScreenPosition);
    
            Vector3 direction = (mouseWorldPosition - _container.BallHoldPoint.position);
            direction.y = 0;
            direction = direction.normalized;
    
            _currentBallSystem.SetDirection(direction);
            _currentBallSystem.SetVelocity(_container.BallSpeed);
            _gameContext.AddGameSystem(_currentBallSystem);
        }
        
        private Vector3 ScreenToWorldPosition(Vector2 screenPosition)
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y, 0));
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