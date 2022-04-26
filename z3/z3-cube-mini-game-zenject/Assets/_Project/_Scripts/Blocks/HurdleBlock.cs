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
    public class HurdleBlock : FallingBlock
    {
        public class HurdleBlockPool : MonoMemoryPool<HurdleBlock>
        {
            protected override void OnSpawned(HurdleBlock block)
            {
                base.OnSpawned(block);
                block.Spawn(BlockType.Hurdle);
            }

            protected override void OnDespawned(HurdleBlock block)
            {
                base.OnDespawned(block);
                block.Despawn();
            }
        }
    }
}
