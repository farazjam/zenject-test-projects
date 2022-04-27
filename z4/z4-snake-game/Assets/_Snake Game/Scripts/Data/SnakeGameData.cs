using UnityEngine;

namespace Snake.Scripts.Data
{
    [CreateAssetMenu(fileName = "SnakeGameData", menuName = "Snake/SnakeGameData")]
    public class SnakeGameData : ScriptableObject
    {
        public Vector2Int mapSize;
        public Vector2Int startPosition;
        public float secondsPerTile;
        public int maxFoodPerLevel;
        public Vector2Int foodSpawnInterval;
        public Vector2Int hurdleSpawnInterval;
    }
}

