using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;

namespace Cube.MiniGame.Systems
{
    public class GameStateHandler : MonoBehaviour, IGameSystem
    {
        private DataManager _data;
        private bool _isActive;
        public bool IsActive => _isActive;
        private GameState _state;
        public static event Action<LevelConclusion, int> LevelConclude;
        public static event Action<int> ScoreUpdate;
        public static event Action<int> LevelUpdate;

        private void OnEnable()
        { 
            PlayerBlock.BlockTouchedPlayer += OnBlockTouchedPlayer;
            GameManager.SystemStateChanged += OnSystemStateChanged;
        }

        private void OnDisable() 
        {
            PlayerBlock.BlockTouchedPlayer += OnBlockTouchedPlayer;
            GameManager.SystemStateChanged -= OnSystemStateChanged;
        }

        public void Start()
        {
            Assert.IsNotNull(DataManager.Instance);
            _data = DataManager.Instance;
        }

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
            Debug.Log($"{gameObject.name} System :{state}");
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
            _data.Score = 0;
            UpdateScore();
            UpdateLevel();
            _state = GameState.Idle;
        }

        private void OnBlockTouchedPlayer(BlockType type)
        {
            if (!_isActive) return;
            if (type == BlockType.Coin) AddScore();
            else if (type == BlockType.Hurdle) LevelFailed();
        }

        public void AddScore()
        {
            _data.Score++;
            UpdateScore();
            if (_data.Score >= _data.Data.game.MaxCoinsPerLevel)
            {
                _data.Level++;
                LevelComplete();
            }
        }

        private void UpdateScore() => ScoreUpdate?.Invoke(_data.Score);
        private void UpdateLevel() => LevelUpdate?.Invoke(_data.Level);
        public void LevelComplete() => LevelConclude?.Invoke(LevelConclusion.Completed, _data.Level);
        public void LevelFailed() => LevelConclude?.Invoke(LevelConclusion.Failed, _data.Level);

        
    }
}
