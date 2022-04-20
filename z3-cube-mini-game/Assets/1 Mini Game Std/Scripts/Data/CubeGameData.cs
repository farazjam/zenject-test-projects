using UnityEngine;
using System;

namespace Cube.MiniGame.Data
{
    [CreateAssetMenu(fileName = "CubeGameData", menuName = "CubeGame/CubeGameData")]
    public class CubeGameData : ScriptableObject
    {
        public Player player;
        public Coin coin;

        [Serializable]
        public class Player
        {
            public int SpawnPositionY;
            public int SwerveSpeed;
            public int SwerveLimitX;
        }

        [Serializable]
        public class Coin
        {
            public Vector2Int SpawnInterval;
            public int SpawnPositionY;
        }
    }
}

