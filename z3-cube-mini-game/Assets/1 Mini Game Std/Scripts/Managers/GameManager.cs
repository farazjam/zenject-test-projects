using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using System;

namespace Cube.MiniGame.Systems
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private SystemState _state;
        public static event Action<SystemState> SystemStateChanged;

        private void Awake() => Instance = this;
        private void OnEnable() => GameStateHandler.LevelConclude += OnLevelConcluded;
        private void OnDisable() => GameStateHandler.LevelConclude -= OnLevelConcluded;
        public SystemState State
        {
            get { return _state; }
            private set
            {
                if (_state == value) return;
                _state = value;
                SystemStateChanged?.Invoke(_state);
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
        void OnLevelConcluded(LevelConclusion __, int _) => StopGame();

        /*private void OnGUI()
        {
            *//*GUI.skin.label.fontSize = 24;
            GUI.skin.button.fontSize = 24;
            if (GUILayout.Button("Start Game")) StartGame();
            if (GUILayout.Button("End Game")) StopGame();
            GUILayout.Label("Last Direction = " + FindObjectOfType<InputManager>().Direction);*//* // Temp, sorry
            //GUILayout.Label("Score = " + GameStateHandler.Instance.Score);
            //GUILayout.Label("Occupancy Dict = " + OccupancyHandler.Instance._occupancy.Count);
            //GUILayout.Label("Food Dict = " + FoodManager.Instance._spawnedBlocks.Count);
            //GUILayout.Label("Hurdle Dict = " + HurdleManager.Instance._spawnedBlocks.Count);
        }*/
    }
}
