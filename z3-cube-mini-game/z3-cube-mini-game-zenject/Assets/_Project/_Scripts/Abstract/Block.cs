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
        protected Vector3 _defaultPosition;
        public Vector3 DefaultPosition => _defaultPosition;
        protected CubeGameData Data;

        public virtual void Spawn()
        {
            if (Data == null)
            {
                Data = DataManager.Instance.Data;
                Assert.IsNotNull(Data);
            }
            this.gameObject.SetActive(true);
        }

        public virtual void Despawn()
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

        public void SetPosition(Vector3 pos) => transform.localPosition = pos;
        public void ResetPosition() => SetPosition(DefaultPosition);
    }
}
