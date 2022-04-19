using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cube.Spawner.DI1
{
    public class SpawnerDI1 : MonoBehaviour
    {
        [Inject] FactoryDI1 factory;

        void Update()
        {
            if (Input.GetMouseButtonUp(0)) Spawn();
        }

        void Spawn()
        {
            GameObject cube = factory.Create().gameObject;
            Destroy(cube, 2f);
        }
    }
}
