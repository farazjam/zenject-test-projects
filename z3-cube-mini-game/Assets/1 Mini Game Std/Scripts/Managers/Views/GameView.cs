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
            GameManager.SystemStateChanged += OnSystemStateChanged;
        }

        private void OnDisable()
        {
            GameManager.SystemStateChanged -= OnSystemStateChanged;
        }

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Clear) Clear();
        }

        void OnLevelUpdated(int level) => textLevel.text = $"Level = {level}";
        void OnScoreUpdated(int score) => textScore.text = $"Score = {score}";
        void OnLevelConcluded(LevelConclusion levelConclusion, int level) => textLevel.text = $"Level {level} {levelConclusion}";
        void Clear() => textLevel.text = textScore.text = "";
    }
}
