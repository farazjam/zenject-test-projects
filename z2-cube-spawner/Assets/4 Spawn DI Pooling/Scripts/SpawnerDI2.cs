using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cube.Spawner.DI2
{
    public class SpawnerDI2 : MonoBehaviour
    {
        [Inject] Cube.CubePool _cubePool;

        void Update()
        {
            if (Input.GetMouseButtonUp(0)) Spawn();
        }

        void Spawn()
        {
            Cube cube = _cubePool.Spawn();
            cube.transform.localPosition = Vector3.zero;
            StartCoroutine(AutoDespawn(cube));
        }

        IEnumerator AutoDespawn(Cube cube)
        {
            yield return new WaitForSeconds(2f);
            _cubePool.Despawn(cube);
        }
        
    }
}
