using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using Cube.MiniGame.Systems;
using Cube.MiniGame.Utils;
using UnityEngine.Assertions;
using System;
using Zenject;

namespace Cube.MiniGame.Abstract
{
    [RequireComponent(typeof(Rigidbody))]
    public class FallingBlock : Block
    {
        [Inject] IUtil _util;
        private Rigidbody _rigidBody;

        private void Awake() => _rigidBody = GetComponent<Rigidbody>();

        public new void Spawn(BlockType type)
        {
            base.Spawn(type);
            transform.position = _util.GetRandomPosition(type);
            _rigidBody.isKinematic = false;
        }

        public new void Despawn()
        {
            base.Despawn();
            if (_rigidBody) _rigidBody.Sleep();
        }
    }
}
