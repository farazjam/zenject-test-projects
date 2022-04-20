using UnityEngine;
using System;

namespace Cube.MiniGame.Data
{
    [CreateAssetMenu(fileName = "CubeGameData", menuName = "CubeGame/CubeGameData")]
    public class CubeGameData : ScriptableObject
    {
        public PlayerData player;
        public GameSettings settings;

        [Serializable]
        public class PlayerData
        {
            public int SpawnPositionY;
            public int SwerveSpeed;
            public int SwerveLimitX;
        }

        [Serializable]
        public class GameSettings
        {
            public Vector2Int coinSpawnInterval;
        }
    }
}

