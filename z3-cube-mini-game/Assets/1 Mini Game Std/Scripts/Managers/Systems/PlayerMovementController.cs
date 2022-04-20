using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cube.MiniGame.Data;
using System;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class PlayerMovementController : MonoBehaviour, IGameSystem
    {
        private bool _isActive;
        public bool IsActive => _isActive;
        [SerializeField] GameObject player;

        private void OnEnable() 
        { 
            GameManager.SystemStateChanged += OnSystemStateChanged;
            InputManager.InputReceived += Move;
        }
        private void OnDisable() 
        { 
            GameManager.SystemStateChanged -= OnSystemStateChanged;
            InputManager.InputReceived -= Move;
        }

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
        }

        void Start() => Assert.IsNotNull(player);

        public void StartSystem()
        {
             if (_isActive) return;
             _isActive = true;
             Debug.Log("PlayerMovementController.StartSystem");
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            Debug.Log("PlayerMovementController.StopSystem");
        }

        public void ClearSystem()
        {
            player.transform.position = Vector3.zero;
            Debug.Log("PlayerMovementController.ClearSystem");
        }

        void Move(Direction direction)
        {
            if (!_isActive) return;
            if (direction == Direction.Left) player.transform.position += Vector3.left * Time.deltaTime * 10f;
            else if (direction == Direction.Right) player.transform.position += Vector3.right * Time.deltaTime * 10f;
        }
    }
}
