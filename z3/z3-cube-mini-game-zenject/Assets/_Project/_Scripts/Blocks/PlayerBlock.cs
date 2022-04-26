using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Abstract;
using Cube.MiniGame.Data;
using Cube.MiniGame.Systems;
using System;
using Zenject;

namespace Cube.MiniGame.Blocks
{
    public interface IPlayer 
    {
        void Spawn();
        void Despawn();
    }

    public class PlayerBlock : Block, IPlayer
    {
        [Inject] private CubeGameData _data;
        [Inject] private SignalBus _signalBus;

        public void Spawn()
        {
            base.Spawn(BlockType.Player);
            transform.position = new Vector3(0, _data.player.SpawnPositionY, 0);
        }

        public void Despawn() => base.Despawn();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Block block))
            {
                _signalBus.TryFire(new BlockTouchedSignal(BlockType.Player, block.Type, block));
            }
        }
    }
}
