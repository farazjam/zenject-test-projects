using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Data;
using Snake.Scripts.Abstract;
using System;

namespace Snake.Scripts.Systems
{
    public class InputManager : MonoBehaviour, IGameSystem
    {
        public static InputManager Instance;
        private Direction _currentDirection;
        private Direction _newDirection;
        public static event Action<Direction> DirectionChanged;
        private bool _isActive;
        private float _secondsPerTile;
        private float _builtUpTime;

        public Direction Direction
        {
            get { return _currentDirection; }
            private set
            {
                if (value == _currentDirection) return;
                _currentDirection = value;
                DirectionChanged?.Invoke(_currentDirection);
            }
        }

        private void Awake() => Instance = this;
        private void OnEnable() => GameManager.SystemStateChanged += OnSystemStateChanged;
        private void OnDisable() => GameManager.SystemStateChanged -= OnSystemStateChanged;
        public bool IsActive => _isActive;

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
        }

        void Start() 
        {
            _secondsPerTile = DataManager.Instance.GameData.secondsPerTile;
            _isActive = false;
            _builtUpTime = 0f;
        }

        void Update()
        {
            if (!_isActive) return;
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.W)) _newDirection = Direction.Up;
                else if (Input.GetKeyDown(KeyCode.A)) _newDirection = Direction.Left;
                else if (Input.GetKeyDown(KeyCode.S)) _newDirection = Direction.Down;
                else if (Input.GetKeyDown(KeyCode.D)) _newDirection = Direction.Right;
                else _newDirection = Direction.None;

                if (_newDirection != Direction.None &&
                    _currentDirection != Map.Instance.Invert(_newDirection) && _builtUpTime >= _secondsPerTile)
                {
                    Direction = _newDirection;
                    _builtUpTime = 0f;
                }
            }
            _builtUpTime += Time.deltaTime;
        }

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
            Debug.Log("InputManager.StartSystem");
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            Debug.Log("InputManager.StopSystem");
        }

        public void ClearSystem()
        {
            _newDirection = Direction.None;
            Direction = _newDirection;
            _currentDirection = Direction.Up;
            _builtUpTime = 0;
            Debug.Log("InputManager.ClearSystem");
        }
    }
}
