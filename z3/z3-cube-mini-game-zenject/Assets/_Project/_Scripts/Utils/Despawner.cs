using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Abstract;
using Cube.MiniGame.Systems;
using Cube.MiniGame.Data;
using UnityEngine.Assertions;
using Cube.MiniGame.Blocks;
using Zenject;
using System;

namespace Cube.MiniGame.Utils
{
    [RequireComponent(typeof(BoxCollider))]
    public class Despawner : MonoBehaviour
    {
        private BoxCollider _collider;
        [Inject] SignalBus _signalBus;

        private void OnEnable() => _signalBus.Subscribe<SystemStateChangedSignal>(OnSystemStateChanged);
        private void OnDisable() => _signalBus.Unsubscribe<SystemStateChangedSignal>(OnSystemStateChanged);

        private void Start()
        {
            _collider = GetComponent<BoxCollider>();
            Assert.IsNotNull(_collider);
            _collider.isTrigger = true;
        }

        public void OnSystemStateChanged(SystemStateChangedSignal args)
        {
            _collider.enabled = (args.state == SystemState.Start) ? true : false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Block block))
            {
                _signalBus.TryFire(new BlockTouchedSignal(BlockType.None, block.Type, block));
            }
        }
    }
}