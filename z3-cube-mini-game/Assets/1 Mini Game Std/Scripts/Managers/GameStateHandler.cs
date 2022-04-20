using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cube.MiniGame.Data;
using UnityEngine.Assertions;

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
        public void Start()
        {
            Assert.IsNotNull(DataManager.Instance);
            _data = DataManager.Instance;
        }

        /*public void AddScore()
        {
            _data.Score++;
            UpdateScore();
            if (_data.Score >= _data.GameData.maxFoodPerLevel)
            {
                _data.Level++;
                LevelComplete();
            }
        }*/

        public void Reset()
        {
            _data.Score = 0;
            UpdateScore();
            UpdateLevel();
        }

        private void UpdateScore() => ScoreUpdate?.Invoke(_data.Score);
        private void UpdateLevel() => LevelUpdate?.Invoke(_data.Level);

        public void LevelComplete() => LevelConclude?.Invoke(LevelConclusion.Completed, -1/*_data.Level*/);

        public void LevelFailed() => LevelConclude?.Invoke(LevelConclusion.Failed, -1/*_data.Level*/);

        
    }
}
