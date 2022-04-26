using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using UnityEngine;

namespace Cube.MiniGame.Abstract
{
    public interface IBlock
    {
        BlockType Type { get; }
        Vector3 DefaultPosition { get; }

        void Spawn(BlockType type);
        void Despawn();
    }
}
