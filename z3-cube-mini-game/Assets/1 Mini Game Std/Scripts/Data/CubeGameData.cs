using UnityEngine;
using System;

namespace Cube.MiniGame.Data
{
    [CreateAssetMenu(fileName = "CubeGameData", menuName = "CubeGame/CubeGameData")]
    public class CubeGameData : ScriptableObject
    {
        public PlayerData Player;

        [Serializable]
        public class PlayerData
        {
            public int SpawnPositionY;
            public int SwerveSpeed;
            public int SwerveLimitX;
        }
    }
}

