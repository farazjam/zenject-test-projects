using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using Cube.MiniGame.Systems;
using UnityEngine.Assertions;

namespace Cube.MiniGame.Abstract
{
    public abstract class Block : MonoBehaviour, IBlock
    {
        protected BlockType _type;
        public BlockType Type => _type;
        protected CubeGameData Data;

        public virtual void Spawn()
        {
            if (Data == null)
            {
                Data = DataManager.Instance.GameData;
                Assert.IsNotNull(Data);
            }
            this.gameObject.SetActive(true);
        }

        public virtual void Despawn()
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
