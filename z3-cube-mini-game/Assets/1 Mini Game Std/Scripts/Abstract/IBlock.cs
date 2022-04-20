using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;

namespace Cube.MiniGame.Abstract
{
    public interface IBlock
    {
        BlockType Type { get; }
        void Spawn();
        void Despawn();
    }
}
