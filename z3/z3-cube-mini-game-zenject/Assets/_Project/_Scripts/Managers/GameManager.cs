using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using System;
using Zenject;

namespace Cube.MiniGame.Systems
{
    public interface IGameManager
    {
        void StartGame();
        void StopGame();
    }

    public class GameManager : IInitializable, IDisposable, IGameManager
    {
        private SystemState _state;
        private readonly SignalBus _signalBus;

        public GameManager(SignalBus signalBus) => _signalBus = signalBus;

        public void Initialize() => _signalBus.Subscribe<LevelConclusionSignal>(OnLevelConcluded);
        public void Dispose() => _signalBus.Unsubscribe<LevelConclusionSignal>(OnLevelConcluded);

        public SystemState State
        {
            get { return _state; }
            private set
            {
                if (_state == value) return;
                _state = value;
                _signalBus.TryFire(new SystemStateChangedSignal(_state));
                Debug.Log($"--- SystemState :{_state} ---");
            }
        }

        private void StartSystems()
        {
            State = SystemState.Clear;
            State = SystemState.Start;
        }

        private void StopSystems() => State = SystemState.Stop;
        public void StartGame() => StartSystems();
        public void StopGame() => StopSystems();
        void OnLevelConcluded(LevelConclusionSignal args) => StopGame();
    }
}
