using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Abstract;
using Cube.MiniGame.Data;
using Cube.MiniGame.Systems;
using Random = UnityEngine.Random;

namespace Cube.MiniGame.Blocks
{
    public class HurdleBlock : Block
    {
        public override void Spawn()
        {
            base.Spawn();
            _type = BlockType.Hurdle;
            _defaultPosition = new Vector3(Random.Range(-Data.hurdle.SpawnRangeX, Data.hurdle.SpawnRangeX), Data.hurdle.SpawnPositionY, 0);
            SetPosition(_defaultPosition);
        }

        public override void Despawn() => base.Despawn();
    }
}
