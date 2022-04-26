using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using UnityEngine.Assertions;
using Zenject;

namespace Cube.MiniGame.Systems
{
    public interface IDataManager 
    {
        int Level { get; set; }
        int Score { get; set; }
    }

    [DefaultExecutionOrder((int)ExecutionOrder.BeforeSystem)]
    public class DataManager :  IInitializable, IDataManager
    {
        private const string CurrentLevel = "CurrentLevel";
        private int _level;
        private int _score;

        public void Initialize()
        {
            _level = PlayerPrefs.GetInt(CurrentLevel, 1);
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
