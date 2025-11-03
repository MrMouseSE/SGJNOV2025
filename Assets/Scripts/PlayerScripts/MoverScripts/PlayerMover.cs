using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts.MoverLogic
{
    public class PlayerMover : IDisposable
    {
        private readonly InputSystemActions _inputSystem;
        private readonly float _speed;
        private readonly Transform _playerTransform;
        
        private Vector2 _direction;
        private bool _isMoving;
        private readonly float _leftBound;
        private readonly float _rightBound;

        public PlayerMover(InputSystemActions inputSystem, Transform playerTransform, float speed, float leftBound)
        {
            _inputSystem = inputSystem;
            _playerTransform = playerTransform;
            _speed = speed;
            SubscribeInputEvents();
            
            _leftBound = leftBound;
            _rightBound = -_leftBound;
        }

        public void Dispose()
        {
            UnsubscribeInputEvents();
        }
        
        public void Move()
        {
            if (_isMoving == false)
                return;
            
            float newPosition = _playerTransform.position.x + _direction.x * _speed;
    
            if (newPosition < _leftBound || newPosition > _rightBound)
                return;
            
            _playerTransform.position = 
                new Vector3(newPosition, _playerTransform.position.y, _playerTransform.position.z);
        }

        private void SubscribeInputEvents()
        {
            _inputSystem.Player.Move.started += GetDirectionToMove;
            _inputSystem.Player.Move.canceled += ResetDirection;

        }

        private void UnsubscribeInputEvents()
        {
            _inputSystem.Player.Move.started -= GetDirectionToMove;
            _inputSystem.Player.Move.canceled -= ResetDirection;
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