using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;

namespace Cube.MiniGame.Abstract
{
    public interface IBlock
    {
        BlockType Type { get; }
        Vector3 DefaultPosition { get; }
        void Spawn();
        void Despawn();
        void SetPosition(Vector3 pos);
        void ResetPosition();
    }
}
