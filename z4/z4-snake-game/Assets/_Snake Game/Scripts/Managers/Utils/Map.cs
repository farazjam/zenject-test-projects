using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Data;
using System;
using Random = UnityEngine.Random;

namespace Snake.Scripts.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.BeforeSystem)]
    public class Map : MonoBehaviour
    {
        public static Map Instance;
        private SnakeGameData _data;

        private void Awake() => Instance = this;
        private void Start() => _data = DataManager.Instance.GameData;
        public SnakeGameData Data => _data;

        public bool IsCoordinateValid(Vector2Int coordinate)
        {
            return coordinate.x >= 0 && coordinate.x < _data.mapSize.x && coordinate.y >= 0 &&
                   coordinate.y < _data.mapSize.y;
        }

        public Vector2Int DirectionToVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector2Int.up;
                case Direction.Right:
                    return Vector2Int.right;
                case Direction.Down:
                    return Vector2Int.down;
                case Direction.Left:
                    return Vector2Int.left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public Direction Invert(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public Vector2Int GetRandomCoordinate()
        {
            return new Vector2Int(Random.Range(0, _data.mapSize.x), Random.Range(0, _data.mapSize.y));
        }

        public Vector3 ToWorldPosition(Vector2Int coordinate)
        {
            var worldHalfX = (_data.mapSize.x + 1f) / 2f;
            var worldHalfY = (_data.mapSize.y + 1f) / 2f;
            return new Vector3(coordinate.x - worldHalfX + 1, 0, coordinate.y - worldHalfY + 1); ;
        }

        public Vector2Int GetNextCoordinate(Vector2Int coordinate, Direction direction)
        {
            var delta = DirectionToVector(direction);
            var newPosition = coordinate + delta;
            if (newPosition.x < 0) newPosition.x += _data.mapSize.x;
            else if (newPosition.x >= _data.mapSize.x) newPosition.x %= _data.mapSize.x;
            if (newPosition.y < 0) newPosition.y += _data.mapSize.y;
            else if (newPosition.y >= _data.mapSize.y) newPosition.y %= _data.mapSize.y;
            return newPosition;
        }
    }
}
