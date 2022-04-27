using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Data;
using Snake.Scripts.Systems;
using Snake.Scripts.Abstract;

namespace Snake.Scripts.Blocks
{
    public class SnakeBlock : Block
    {
        public override void Spawn()
        {
            base.Spawn();
            _type = BlockType.Snake;
            ResetPosition();
            OccupancyHandler.Instance.Occupy(_coordinate, _type);
        }

        public override void Despawn() => ResetPosition();

        void ResetPosition()
        {
            _coordinate = DataManager.Instance.GameData.startPosition;
            transform.position = Map.Instance.ToWorldPosition(_coordinate);
        }

        public void Move(Vector2Int targetPosition)
        {
            OccupancyHandler.Instance.UnOccupy(_coordinate);
            _coordinate = targetPosition;
            transform.position = Map.Instance.ToWorldPosition(_coordinate);
            OccupancyHandler.Instance.Occupy(_coordinate, BlockType.Snake);
        }
    }
}
