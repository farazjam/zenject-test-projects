using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using Cube.MiniGame.Systems;
using UnityEngine.Assertions;

namespace Cube.MiniGame.Abstract
{
    public class Block : MonoBehaviour, IBlock
    {
        protected BlockType _type;
        protected Vector3 _defaultPosition = Vector3.zero;

        public BlockType Type => _type;
        public Vector3 DefaultPosition => _defaultPosition;

        public void Spawn(BlockType type)
        {
            _type = type;
            if (!gameObject.activeInHierarchy) gameObject.SetActive(true);
        }

        public void Despawn()
        {
            transform.position = _defaultPosition;
            if (gameObject.activeInHierarchy) gameObject.SetActive(false);
        }
    }
}
