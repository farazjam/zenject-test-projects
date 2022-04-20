using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Abstract;
using Cube.MiniGame.Data;
using Cube.MiniGame.Systems;

namespace Cube.MiniGame.Blocks
{
    public class PlayerBlock : Block
    {
        private void Awake() => _type = BlockType.Player;

        public override void Spawn()
        {
            base.Spawn();
            ResetPosition();
        }

        public override void Despawn()
        {
            base.Despawn();
        }

        void ResetPosition() => transform.localPosition = new Vector3(0, DataManager.Instance.GameData.playerSpawnPositionY, 0);

        public void Move(Direction direction)
        {
            if (direction == Direction.Left) transform.localPosition += Vector3.left * Time.deltaTime * DataManager.Instance.GameData.playerMoveSpeed;
            else if (direction == Direction.Right) transform.localPosition += Vector3.right * Time.deltaTime * DataManager.Instance.GameData.playerMoveSpeed;
        }
    }
}
