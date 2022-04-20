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
        CubeGameData _data;
        [SerializeField] PlayerBlock playerBlock;
        Vector3 _position;

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

        public void Move(Direction direction)
        {
            if (!_isActive) return;
            _position = playerBlock.transform.localPosition;
            if (direction == Direction.Left) _position += Vector3.left * Time.deltaTime * _data.player.SwerveSpeed;
            else if (direction == Direction.Right) _position += Vector3.right * Time.deltaTime * _data.player.SwerveSpeed;
            Clamp();
            playerBlock.SetPosition(_position);
        }

        private void Clamp()
        {
            if (_position.x < -_data.player.SwerveLimitX) _position = new Vector3(-_data.player.SwerveLimitX, _position.y, _position.z);
            else if (_position.x > _data.player.SwerveLimitX) _position = new Vector3(_data.player.SwerveLimitX, _position.y, _position.z);
        }
    }
}
