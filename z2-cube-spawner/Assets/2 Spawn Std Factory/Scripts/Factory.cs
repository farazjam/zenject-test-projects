using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cube.Spawner.StandardFactory
{
    public class Factory : MonoBehaviour
    {
        public static Factory Instance;
        [SerializeField] GameObject prefab;

        private void Awake()
        {
            Instance = this;
            Assert.IsNotNull(prefab);
        }

        public GameObject GetNewObject()
        {
            return Instantiate(prefab);
        }

    }
}
