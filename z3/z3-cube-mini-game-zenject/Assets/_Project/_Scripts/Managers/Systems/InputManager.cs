using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cube.MiniGame.Data;
using Zenject;

namespace Cube.MiniGame.Systems
{
    public interface IInputListener
    {
        void MoveTo(Direction direction);
    }

    public class InputManager : IInitializable, ITickable, IDisposable, IGameSystem
    {
        private bool _isActive;
        private Direction _direction;
        private IInputListener _inputListener;
        private SignalBus _signalBus;

        public bool IsActive => _isActive;

        public Direction Direction
        {
            get { return _direction; }
            private set
            {
                _direction = value;
                _inputListener.MoveTo(_direction);
            }
        }

        public InputManager(IInputListener inputListener, SignalBus signalBus)
        {
            _inputListener = inputListener;
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
            Debug.Log($"InputManager System :{state}");
        }

        public void Tick()
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
