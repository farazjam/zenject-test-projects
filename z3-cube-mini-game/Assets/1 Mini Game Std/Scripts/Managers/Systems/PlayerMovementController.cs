using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using System;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class PlayerMovementController : MonoBehaviour, IGameSystem
    {
        private bool _isActive;
        public bool IsActive => _isActive;
        [SerializeField] PlayerBlock playerBlock;

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

        void Start() => Assert.IsNotNull(playerBlock);

        public void StartSystem()
        {
             if (_isActive) return;
             _isActive = true;
            playerBlock.Spawn();
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
            playerBlock.Despawn();
            Debug.Log("PlayerMovementController.ClearSystem");
        }

        void Move(Direction direction)
        {
            if (!_isActive) return;
            playerBlock.Move(direction);
        }
    }
}
