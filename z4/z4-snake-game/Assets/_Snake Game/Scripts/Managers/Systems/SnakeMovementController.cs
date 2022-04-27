using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Snake.Scripts.Data;
using Snake.Scripts.Blocks;
using Snake.Scripts.Abstract;
using System;

namespace Snake.Scripts.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class SnakeMovementController : MonoBehaviour, IGameSystem
    {
        private bool _isActive;
        [SerializeField] SnakeBlock headBlock;
        private float _secondsPerTile;
        public static event Action<Vector2Int> BeforeSnakeMoved;
        public static event Action<Vector2Int> AfterSnakeMoved;

        private void OnEnable() => GameManager.SystemStateChanged += OnSystemStateChanged;
        private void OnDisable()
        {
            GameManager.SystemStateChanged -= OnSystemStateChanged;
            StopCoroutine(MovementLoop());
        }

        void Start()
        {
            Assert.IsNotNull(headBlock);
            _secondsPerTile = DataManager.Instance.GameData.secondsPerTile;
        }

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
        }

        public bool IsActive => _isActive;

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
            headBlock.Spawn();
            StartCoroutine(MovementLoop());
            Debug.Log("SnakeMovementController.StartSystem");
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            StopCoroutine(MovementLoop());
            Debug.Log("SnakeMovementController.StopSystem");
        }

        public void ClearSystem()
        {
            headBlock.Despawn();
            Debug.Log("SnakeMovementController.ClearSystem");
        }

        IEnumerator MovementLoop()
        {
            if (!_isActive) yield return null;
            while (_isActive)
            {
                var nextPosition = Map.Instance.GetNextCoordinate(headBlock.Coordinate, InputManager.Instance.Direction);
                BeforeSnakeMoved?.Invoke(nextPosition);
                headBlock.Move(nextPosition);
                AfterSnakeMoved?.Invoke(nextPosition);
                yield return new WaitForSeconds(_secondsPerTile);
            }
        }
    }
}
