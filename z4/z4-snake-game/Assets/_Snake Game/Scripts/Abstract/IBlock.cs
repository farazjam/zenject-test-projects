using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Scripts.Abstract
{
    public interface IBlock
    {
        Vector2Int Coordinate { get; }
        void Spawn();
        void Despawn();
    }
}
