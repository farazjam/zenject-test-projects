using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cube.MiniGame.Data;

namespace Cube.MiniGame.Systems
{
    public class InputManager : MonoBehaviour, IGameSystem
    {
        private Direction _direction;
        public static event Action<Direction> InputReceived;
        private bool _isActive;
        public bool IsActive => _isActive;
        public Direction Direction
        {
            get { return _direction; }
            private set
            {
                _direction = value;
                InputReceived?.Invoke(_direction);
            }
        }

        private void OnEnable() => GameManager.SystemStateChanged += OnSystemStateChanged;
        private void OnDisable() => GameManager.SystemStateChanged -= OnSystemStateChanged;
        
        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
            Debug.Log($"{gameObject.name} System :{state}");
        }

        void Update()
        {
            if (!_isActive) return;
            if (Input.anyKey)
            {
                if (Input.GetKey(KeyCode.A)) _direction = Direction.Left;
                else if (Input.GetKey(KeyCode.D)) _direction = Direction.Right;
                else _direction = Direction.None;

                if (_direction != Direction.None) Direction = _direction;
            }
        }

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
        }

        public void ClearSystem() => Direction = Direction.None;
    }
}
