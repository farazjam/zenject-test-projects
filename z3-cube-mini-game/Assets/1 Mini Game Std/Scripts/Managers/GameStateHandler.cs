using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;

namespace Cube.MiniGame.Systems
{
    public class GameStateHandler : MonoBehaviour
    {
        public static GameStateHandler Instance;
        private DataManager _data;
        
        public static event Action<LevelConclusion, int> LevelConclude;
        public static event Action<int> ScoreUpdate;
        public static event Action<int> LevelUpdate;


        public void Awake() => Instance = this;
        private void OnEnable() => PlayerBlock.BlockTouchedPlayer += OnBlockTouchedPlayer;
        private void OnDisable() => PlayerBlock.BlockTouchedPlayer += OnBlockTouchedPlayer;
        public void Start()
        {
            Assert.IsNotNull(DataManager.Instance);
            _data = DataManager.Instance;
        }

        public void AddScore()
        {
            _data.Score++;
            UpdateScore();
            /*if (_data.Score >= _data.GameData.maxFoodPerLevel)
            {
                _data.Level++;
                LevelComplete();
            }*/
        }

        public void Reset()
        {
            _data.Score = 0;
            UpdateScore();
            UpdateLevel();
        }

        private void OnBlockTouchedPlayer(BlockType type)
        {
            if (type == BlockType.Coin) AddScore();
            //else if (type == BlockType.Hurdle) ;//Gameover
        }

        private void UpdateScore() => ScoreUpdate?.Invoke(_data.Score);
        private void UpdateLevel() => LevelUpdate?.Invoke(_data.Level);

        public void LevelComplete() => LevelConclude?.Invoke(LevelConclusion.Completed, -1/*_data.Level*/);

        public void LevelFailed() => LevelConclude?.Invoke(LevelConclusion.Failed, -1/*_data.Level*/);

        
    }
}
