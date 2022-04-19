using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.Spawner.Standard
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject cubePrefab;

        void Update()
        {
            if (Input.GetMouseButtonUp(0)) Spawn();
        }

        void Spawn()
        {
            GameObject cube = Instantiate(cubePrefab);
            Destroy(cube, 2f);
        }
    }
}
