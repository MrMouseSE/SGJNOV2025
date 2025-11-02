using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerModel : IDisposable
    {
        private readonly InputSystemActions _inputSystem;
        private readonly PlayerContainer _playerContainer;
        private readonly GameContext _gameContext;
        private readonly float _leftBound;
        private readonly float _rightBound;
        
        private Vector2 _direction;
        private bool _isMoving;
        
        public PlayerModel(InputSystemActions inputSystem, PlayerContainer playerContainer, GameContext gameContext)
        {
            _inputSystem = inputSystem;
            _inputSystem.Player.Move.started += GetDirectionToMove;
            _inputSystem.Player.Move.canceled += ResetDirection;
            
            _playerContainer = playerContainer;
            
            _gameContext = gameContext;
            
            _leftBound = _gameContext.LeftEndPoint.position.x;
            _rightBound = -_leftBound;
        }

        public void Dispose()
        {
            _inputSystem.Player.Move.started -= GetDirectionToMove;
            _inputSystem.Player.Move.canceled -= ResetDirection;
        }

        public void Move()
        {
            if (_isMoving == false)
                return;
            
            float newPosition = _playerContainer.Transform.position.x + _direction.x * _playerContainer.Speed;
    
            if (newPosition < _leftBound || newPosition > _rightBound)
                return;
            
            _playerContainer.Transform.position = 
                new Vector3(newPosition, _playerContainer.Transform.position.y, _playerContainer.Transform.position.z);
        }

        private void GetDirectionToMove(InputAction.CallbackContext obj)
        {
            _isMoving = true;
            _direction = new Vector2(obj.ReadValue<Vector2>().x, 0);
        }

        private void ResetDirection(InputAction.CallbackContext obj)
        {
            _direction = Vector2.zero;
            _isMoving = false;
        }
    }
}