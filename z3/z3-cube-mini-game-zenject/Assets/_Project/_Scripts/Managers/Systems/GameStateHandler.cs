using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using Zenject;

namespace Cube.MiniGame.Systems
{
    public class GameStateHandler : IInitializable, IDisposable, IGameSystem
    {
        private bool _isActive;
        private readonly IDataManager _dataManager;
        private readonly SignalBus _signalBus;
        private readonly CubeGameData _data;
        private GameState _state;
        public static event Action<int> ScoreUpdate;
        public static event Action<int> LevelUpdate;

        public bool IsActive => _isActive;

        public GameStateHandler(IDataManager dataManager, SignalBus signalBus, CubeGameData data)
        {
            _dataManager = dataManager;
            _signalBus = signalBus;
            _data = data;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SystemStateChangedSignal>(OnSystemStateChanged);
            _signalBus.Subscribe<BlockTouchedSignal>(OnPlayerBlockTouched);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SystemStateChangedSignal>(OnSystemStateChanged);
            _signalBus.Unsubscribe<BlockTouchedSignal>(OnPlayerBlockTouched);
        }

        public void OnSystemStateChanged(SystemStateChangedSignal args)
        {
            SystemState state = args.state;
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
            Debug.Log($"GameStateHandler System :{state}");
        }

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
            _state = GameState.Gameplay;
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            _state = GameState.Gameover;
        }

        public void ClearSystem() => Reset();

        public void Reset()
        {
            _dataManager.Score = 0;
            UpdateScore();
            UpdateLevel();
            _state = GameState.Idle;
        }

        public void OnPlayerBlockTouched(BlockTouchedSignal args)
        {
            if (!_isActive) return;
            if (args.touchedType != BlockType.Player) return;
            if (args.toucherType == BlockType.Coin) AddScore();
            else if (args.toucherType == BlockType.Hurdle) LevelFailed();
        }

        public void AddScore()
        {
            _dataManager.Score++;
            UpdateScore();
            if (_dataManager.Score >= _data.game.MaxCoinsPerLevel)
            {
                _dataManager.Level++;
                LevelComplete();
            }
        }

        private void UpdateScore() => ScoreUpdate?.Invoke(_dataManager.Score);
        private void UpdateLevel() => LevelUpdate?.Invoke(_dataManager.Level);
        public void LevelComplete() => _signalBus.TryFire(new LevelConclusionSignal(LevelConclusion.Completed, _dataManager.Level));
        public void LevelFailed() => _signalBus.TryFire(new LevelConclusionSignal(LevelConclusion.Failed, _dataManager.Level));


    }
}
