using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Abstract;
using Cube.MiniGame.Data;
using Cube.MiniGame.Systems;
using Cube.MiniGame.Utils;
using Random = UnityEngine.Random;
using Zenject;

namespace Cube.MiniGame.Blocks
{
    public class CoinBlock : FallingBlock
    {
        public class CoinBlockPool : MonoMemoryPool<CoinBlock>
        {
            protected override void OnSpawned(CoinBlock block)
            {
                base.OnSpawned(block);
                block.Spawn(BlockType.Coin);
            }

            protected override void OnDespawned(CoinBlock block)
            {
                base.OnDespawned(block);
                block.Despawn();
            }
        }
    }
}
