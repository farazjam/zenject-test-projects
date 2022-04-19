using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.Spawner.StandardFactory
{
    public class SpawnerStdFactory : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonUp(0)) Spawn();
        }

        void Spawn()
        {
            GameObject cube = Factory.Instance.GetNewObject();
            Destroy(cube, 2f);
        }
    }
}
