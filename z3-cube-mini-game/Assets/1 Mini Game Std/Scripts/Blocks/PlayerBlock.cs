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
        public override void Spawn()
        {
            base.Spawn();
            _type = BlockType.Player;
            ResetPosition();
        }

        void ResetPosition() => transform.localPosition = new Vector3(0, Data.player.SpawnPositionY, 0);

        public void Move(Direction direction)
        {
            if (direction == Direction.Left) transform.localPosition += Vector3.left * Time.deltaTime * Data.player.SwerveSpeed;
            else if (direction == Direction.Right) transform.localPosition += Vector3.right * Time.deltaTime * Data.player.SwerveSpeed;
            Clamp();
        }

        private void Clamp()
        {
            Vector3 pos = transform.localPosition;
            if (pos.x < -Data.player.SwerveLimitX) pos = new Vector3(-Data.player.SwerveLimitX, pos.y, pos.z);
            else if (pos.x > Data.player.SwerveLimitX) pos = new Vector3(Data.player.SwerveLimitX, pos.y, pos.z);
            transform.localPosition = pos;
        }
    }
}
