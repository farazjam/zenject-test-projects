using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;

namespace Cube.MiniGame.Abstract
{
    public abstract class Block : MonoBehaviour, IBlock
    {
        protected BlockType _type;
        public BlockType Type => _type;

        public virtual void Spawn() => this.gameObject.SetActive(true);

        public virtual void Despawn()
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
