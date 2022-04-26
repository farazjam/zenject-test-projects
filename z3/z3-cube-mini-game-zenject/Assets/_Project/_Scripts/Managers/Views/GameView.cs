using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Cube.MiniGame.Systems;
using Cube.MiniGame.Data;
using Zenject;

namespace Cube.MiniGame.Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] Text textLevel;
        [SerializeField] Text textScore;
        [SerializeField] Button buttonStart;
        [SerializeField] Button buttonStop;
        [Inject] IGameManager gameManager;
        [Inject] SignalBus _signalBus;

        private void Start()
        {
            Assert.IsNotNull(textLevel);
            Assert.IsNotNull(textScore);
            Assert.IsNotNull(buttonStart);
            Assert.IsNotNull(buttonStop);
            buttonStart.onClick.AddListener(() => gameManager.StartGame());
            buttonStop.onClick.AddListener(() => gameManager.StopGame());
        }

        private void OnEnable()
        {
            GameStateHandler.LevelUpdate += OnLevelUpdated;
            GameStateHandler.ScoreUpdate += OnScoreUpdated;
            _signalBus.Subscribe<LevelConclusionSignal>(OnLevelConcluded);
        }

        private void OnDisable()
        {
            GameStateHandler.LevelUpdate -= OnLevelUpdated;
            GameStateHandler.ScoreUpdate -= OnScoreUpdated;
            _signalBus.Unsubscribe<LevelConclusionSignal>(OnLevelConcluded);
        }

        void OnLevelUpdated(int level) => textLevel.text = $"Level = {level}";
        void OnScoreUpdated(int score) => textScore.text = $"Score = {score}";
        public void OnLevelConcluded(LevelConclusionSignal args) => textLevel.text = $"Level {args.levelNumber} {args.conclusion}";


    }
}
