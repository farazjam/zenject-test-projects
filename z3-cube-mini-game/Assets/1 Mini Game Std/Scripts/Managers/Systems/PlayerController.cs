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
    public class PlayerController : MonoBehaviour, IGameSystem
    {
        private bool _isActive;
        public bool IsActive => _isActive;
        CubeGameData _data;
        [SerializeField] PlayerBlock playerBlock;

        private void OnEnable() => GameManager.SystemStateChanged += OnSystemStateChanged;
        private void OnDisable() => GameManager.SystemStateChanged -= OnSystemStateChanged;

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
        }

        void Start()
        {
            _data = DataManager.Instance.Data;
            Assert.IsNotNull(_data);
            Assert.IsNotNull(playerBlock);
        }

        public void StartSystem()
        {
             if (_isActive) return;
             _isActive = true;
            playerBlock.Spawn();
            Debug.Log("PlayerController.StartSystem");
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            Debug.Log("PlayerController.StopSystem");
        }

        public void ClearSystem()
        {
            playerBlock.Despawn();
            Debug.Log("PlayerController.ClearSystem");
        }
    }
}
