using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using Cube.MiniGame.Abstract;
using System;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class SwerveMovement : MonoBehaviour, IGameSystem
    {
        private bool _isActive;
        public bool IsActive => _isActive;
        CubeGameData _data;
        [SerializeField] Block block;
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
            Debug.Log($"{gameObject.name} System :{state}");
        }

        void Start()
        {
            _data = DataManager.Instance.Data;
            Assert.IsNotNull(_data);
            Assert.IsNotNull(block);
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

        public void ClearSystem() { }

        public void Move(Direction direction)
        {
            if (!_isActive) return;
            _position = block.transform.localPosition;
            if (direction == Direction.Left) _position += Vector3.left * Time.deltaTime * _data.player.SwerveSpeed;
            else if (direction == Direction.Right) _position += Vector3.right * Time.deltaTime * _data.player.SwerveSpeed;
            Clamp();
            block.SetPosition(_position);
        }

        private void Clamp()
        {
            if (_position.x < -_data.player.SwerveLimitX) _position = new Vector3(-_data.player.SwerveLimitX, _position.y, _position.z);
            else if (_position.x > _data.player.SwerveLimitX) _position = new Vector3(_data.player.SwerveLimitX, _position.y, _position.z);
        }
    }
}
