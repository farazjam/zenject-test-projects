using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Data;

namespace Snake.Scripts.Abstract
{
    public abstract class Block : MonoBehaviour, IBlock
    {
        protected BlockType _type;
        protected Vector2Int _coordinate;
        public BlockType Type => _type;
        public Vector2Int Coordinate => _coordinate;

        public virtual void Spawn() => this.gameObject.SetActive(true);

        public virtual void Despawn()
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
