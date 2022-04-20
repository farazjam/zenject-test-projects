using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using System;
using Random = UnityEngine.Random;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class CoinGenerator : MonoBehaviour, IGameSystem
    {
        [SerializeField] CoinBlock coinBlockPrefab;
        private bool _isActive;
        public bool IsActive => _isActive;
        private Vector2Int _coinSpawnInterval;

        private void OnEnable()
        {
            GameManager.SystemStateChanged += OnSystemStateChanged;
        }

        private void OnDisable()
        {
            GameManager.SystemStateChanged -= OnSystemStateChanged;
            StopCoroutine(StartGeneration()); // For safety
        }

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
        }

        void Start()
        {
            Assert.IsNotNull(coinBlockPrefab);
            _coinSpawnInterval = DataManager.Instance.Data.coin.SpawnInterval;
        }

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
            StartCoroutine(StartGeneration());
            Debug.Log("CoinGenerator.StartSystem");
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            StopCoroutine(StartGeneration());
            Debug.Log("CoinGenerator.StopSystem");
        }

        public void ClearSystem()
        {
           /* if (_spawnedBlocks.Count <= 0) return;
            Dictionary<Vector2Int, FoodBlock>.KeyCollection keys = _spawnedBlocks.Keys;
            foreach (Vector2Int pos in keys) _spawnedBlocks[pos].Despawn();
            _spawnedBlocks.Clear();*/
            Debug.Log("CoinGenerator.ClearSystem");
        }

        IEnumerator StartGeneration()
        {
            if (!_isActive) yield return null;
            while (_isActive)
            {
                yield return new WaitForSeconds(Random.Range(_coinSpawnInterval.x, _coinSpawnInterval.y));
                //var position = Map.Instance.GetRandomCoordinate();
                SpawnBlock(Vector3.zero);
            }
        }

        void SpawnBlock(Vector3 position)
        {
            if (!_isActive) return;
            var block = Instantiate(coinBlockPrefab) as CoinBlock;
            block.transform.parent = transform;
            block.transform.localPosition = position;//Map.Instance.ToWorldPosition(position);
            //_spawnedBlocks.Add(position, block);
            block.Spawn();
            //_occupancyHandler.Occupy(position, block.Type);
        }
    }
}
