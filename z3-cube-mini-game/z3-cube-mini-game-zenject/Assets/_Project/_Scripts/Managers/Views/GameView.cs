using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Cube.MiniGame.Systems;
using Cube.MiniGame.Data;

namespace Cube.MiniGame.Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] Text textLevel;
        [SerializeField] Text textScore;
        [SerializeField] Button buttonStart;
        [SerializeField] Button buttonStop;

        private void Start()
        {
            Assert.IsNotNull(textLevel);
            Assert.IsNotNull(textScore);
            Assert.IsNotNull(buttonStart);
            Assert.IsNotNull(buttonStop);
            buttonStart.onClick.AddListener(() => GameManager.Instance.StartGame());
            buttonStop.onClick.AddListener(() => GameManager.Instance.StopGame());
        }

        private void OnEnable()
        {
            GameStateHandler.LevelUpdate += OnLevelUpdated;
            GameStateHandler.ScoreUpdate += OnScoreUpdated;
            GameStateHandler.LevelConclude += OnLevelConcluded;
        }

        private void OnDisable()
        {
            GameStateHandler.LevelUpdate -= OnLevelUpdated;
            GameStateHandler.ScoreUpdate -= OnScoreUpdated;
            GameStateHandler.LevelConclude -= OnLevelConcluded;
        }

        void OnLevelUpdated(int level) => textLevel.text = $"Level = {level}";
        void OnScoreUpdated(int score) => textScore.text = $"Score = {score}";
        void OnLevelConcluded(LevelConclusion levelConclusion, int level) => textLevel.text = $"Level {level} {levelConclusion}";
    }
}
