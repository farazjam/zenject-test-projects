using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// - Spawner doesn't know what objects this factory will instantiate and provide
/// - The type of objects can be changed without changing anything in Spawner
/// - Factory is injected so that we can ask it to create and provide objects
/// </summary>

namespace Cube.Spawner.DI1
{
    public class SpawnerDI1 : MonoBehaviour
    {
        [Inject] FactoryDI1 factory;

        // Way2 (Preferred)
        // private FactoryDI1 factory;
        // [Inject] void Construct(FactoryDI1 factory) => this.factory = factory;

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
