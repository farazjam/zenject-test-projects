using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using UnityEngine.Assertions;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.BeforeSystem)]
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;
        [SerializeField] private CubeGameData _data;
        public CubeGameData GameData => _data;
        private const string CurrentLevel = "CurrentLevel";
        private int _level;
        private int _score;

        void Awake() => Instance = this;

        private void Start()
        {
            Assert.IsNotNull(_data);
            _level = PlayerPrefs.GetInt(CurrentLevel, 0);
            _score = 0;
        }

        public int Level
        {
            get { return _level; }
            set
            {
                if (_level == value) return;
                _level = value;
                PlayerPrefs.SetInt(CurrentLevel, _level);
            }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
    }


}
