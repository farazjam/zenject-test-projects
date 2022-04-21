using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Abstract;
using Cube.MiniGame.Data;
using Cube.MiniGame.Systems;
using System;

namespace Cube.MiniGame.Blocks
{
    public class PlayerBlock : Block
    {
        public static event Action<BlockType> BlockTouchedPlayer;

        public override void Spawn()
        {
            base.Spawn();
            _type = BlockType.Player;
            _defaultPosition = new Vector3(0, Data.player.SpawnPositionY, 0);
            SetPosition(_defaultPosition);
        }

        // Exception for PlayerBlock, object should not be destroyed
        public override void Despawn() => this.gameObject.SetActive(false);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CoinBlock coinBlock))
            {
                BlockTouchedPlayer?.Invoke(BlockType.Coin);
                coinBlock.Despawn();
            }
            else if (other.TryGetComponent(out HurdleBlock hurdleBlock))
            {
                BlockTouchedPlayer?.Invoke(BlockType.Hurdle);
                hurdleBlock.Despawn();
                Despawn();
            }
        }
    }
}
