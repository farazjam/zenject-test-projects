using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Snake.Scripts.Systems;
using Snake.Scripts.Data;

namespace Snake.Scripts.Views
{
    public class GameManagerView : MonoBehaviour
    {
        [SerializeField] Text textScore;
        [SerializeField] Text textLevel;
        [SerializeField] Button buttonStart;
        [SerializeField] Button buttonStop;

        private void OnEnable()
        {
            GameStateHandler.ScoreUpdate += OnScoreUpdated;
            GameStateHandler.LevelUpdate += OnLevelUpdated;
            GameStateHandler.LevelConclude += OnLevelConcluded;
            GameManager.SystemStateChanged += OnSystemStateChanged;
        }

        private void OnDisable()
        {
            GameStateHandler.ScoreUpdate -= OnScoreUpdated;
            GameStateHandler.LevelUpdate -= OnLevelUpdated;
            GameStateHandler.LevelConclude -= OnLevelConcluded;
            GameManager.SystemStateChanged -= OnSystemStateChanged;
        }

        public void OnSystemStateChanged(SystemState state) 
        { 
            if (state == SystemState.Clear) Clear(); 
        }


        private void Start()
        {
            Assert.IsNotNull(textScore);
            Assert.IsNotNull(textLevel);
            Assert.IsNotNull(buttonStart);
            Assert.IsNotNull(buttonStop);
            buttonStart.onClick.AddListener(() => GameManager.Instance.StartGame());
            buttonStop.onClick.AddListener(() => GameManager.Instance.StopGame());
        }

        void OnScoreUpdated(int score) => textScore.text = $"Score = {score}";
        void OnLevelUpdated(int level) => textLevel.text = $"Level = {level}";

        void OnLevelConcluded(LevelConclusion levelConclusion, int level) => textLevel.text = $"Level {level} {levelConclusion}";

        void Clear() => textLevel.text = textScore.text = "";
    }
}