using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Cube.Spawner.DI2
{
    public class Cube : MonoBehaviour
    {
        public class CubePool : MonoMemoryPool<Cube>
        {
            // Called immediately after the item is spawned from the pool
            protected override void OnSpawned(Cube cube)
            {
                base.OnSpawned(cube);
                cube.OnSpawned();
            }

            // Called immediately after the item is despawned from the pool
            protected override void OnDespawned(Cube cube)
            {
                base.OnDespawned(cube);
                cube.OnDespawned();
            }
        }

        private Rigidbody _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            Assert.IsNotNull(_rigidBody);
        }

        public void OnSpawned()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _rigidBody.isKinematic = false;
        }

        public void OnDespawned()
        {
            _rigidBody.isKinematic = true;
        }
    }

    
}
