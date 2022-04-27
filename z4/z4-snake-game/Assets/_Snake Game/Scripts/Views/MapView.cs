using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Systems;
using UnityEngine.Assertions;
using Snake.Scripts.Data;

namespace Snake.Scripts.Views
{
    [DefaultExecutionOrder((int)ExecutionOrder.AfterSystem)]
    public class MapView : MonoBehaviour
    {
        [SerializeField] Transform ground;
        void Start()
        {
            Assert.IsNotNull(ground);
            Vector2Int size = Map.Instance.Data.mapSize;
            ground.localScale = new Vector3(size.x, 1, size.y);
        }
    }
}
