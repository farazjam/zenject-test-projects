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
    public class HurdleGenerator : MonoBehaviour, IGameSystem
    {
        [SerializeField] HurdleBlock hurdleBlockPrefab;
        private bool _isActive;
        public bool IsActive => _isActive;
        private Vector2Int _hurdleSpawnInterval;

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
            Debug.Log($"{gameObject.name} System :{state}");
        }

        void Start()
        {
            Assert.IsNotNull(hurdleBlockPrefab);
            _hurdleSpawnInterval = DataManager.Instance.Data.hurdle.SpawnInterval;
        }

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
            StartCoroutine(StartGeneration());
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            StopCoroutine(StartGeneration());
        }

        public void ClearSystem() { }

        IEnumerator StartGeneration()
        {
            if (!_isActive) yield return null;
            while (_isActive)
            {
                yield return new WaitForSeconds(Random.Range(_hurdleSpawnInterval.x, _hurdleSpawnInterval.y));
                SpawnBlock();
            }
        }

        void SpawnBlock()
        {
            if (!_isActive) return;
            var block = Instantiate(hurdleBlockPrefab, transform) as HurdleBlock;
            block.Spawn();
        }
    }
}
