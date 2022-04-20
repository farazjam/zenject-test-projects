using UnityEngine;

namespace Cube.MiniGame.Data
{
    [CreateAssetMenu(fileName = "CubeGameData", menuName = "CubeGame/CubeGameData")]
    public class CubeGameData : ScriptableObject
    {
        public int playerSpawnPositionY;
        public int playerMoveSpeed;
    }
}

