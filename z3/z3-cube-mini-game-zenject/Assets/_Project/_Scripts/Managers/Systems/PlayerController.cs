using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cube.MiniGame.Abstract;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using System;
using Zenject;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class PlayerController : IInitializable, IDisposable, IGameSystem
    {
        private bool _isActive;
        private IPlayer _playerBlock;
        private SignalBus _signalBus;

        public bool IsActive => _isActive;

        public PlayerController(IPlayer playerBlock, SignalBus signalBus)
        {
            _playerBlock = playerBlock;
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
            Debug.Log($"PlayerController System :{state}");
        }

        public void StartSystem()
        {
             if (_isActive) return;
             _isActive = true;
            _playerBlock.Spawn();
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
        }

        public void ClearSystem() => _playerBlock.Despawn();
    }
}
