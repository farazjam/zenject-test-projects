using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using Cube.MiniGame.Abstract;
using System;
using Zenject;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class SwerveMovement : IInitializable, IDisposable, IGameSystem, IInputListener
    {
        private bool _isActive;
        private IBlock _block;
        private CubeGameData _data;
        private SignalBus _signalBus;
        private Vector3 _position;

        public bool IsActive => _isActive;

        public SwerveMovement(IBlock block, CubeGameData data, SignalBus signalBus)
        {
            _block = block;
            _data = data;
            _signalBus = signalBus;
        }

        public void Initialize() => _signalBus.Subscribe<SystemStateChangedSignal>(OnSystemStateChanged);
        public void Dispose() => _signalBus.Unsubscribe<SystemStateChangedSignal>(OnSystemStateChanged);
        public void OnSystemStateChanged(SystemStateChangedSignal args)
        {
            SystemState state = args.state;
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
            Debug.Log($"SwerveMovement System :{state}");
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

        public void MoveTo(Direction direction)
        {
            if (!_isActive) return;
            _position = ((Block)_block).transform.position;
            if (direction == Direction.Left) _position += Vector3.left * Multiplier();
            else if (direction == Direction.Right) _position += Vector3.right * Multiplier();
            Clamp();
            ((Block)_block).transform.position = _position;
        }

        float Multiplier() => Time.deltaTime * _data.player.SwerveSpeed;

        private void Clamp()
        {
            if (_position.x < -_data.player.SwerveLimitX) _position = new Vector3(-_data.player.SwerveLimitX, _position.y, _position.z);
            else if (_position.x > _data.player.SwerveLimitX) _position = new Vector3(_data.player.SwerveLimitX, _position.y, _position.z);
        }
    }
}
