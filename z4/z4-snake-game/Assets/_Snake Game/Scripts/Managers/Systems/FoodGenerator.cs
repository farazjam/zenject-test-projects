using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Blocks;
using UnityEngine.Assertions;
using Snake.Scripts.Data;
using Snake.Scripts.Abstract;
using Ramdom = UnityEngine.Random;

namespace Snake.Scripts.Systems
{
    public class FoodGenerator : MonoBehaviour, IGameSystem
    {
        public static FoodGenerator Instance;
        private OccupancyHandler _occupancyHandler;
        [SerializeField] private FoodBlock foodBlockPrefab;
        public Dictionary<Vector2Int, FoodBlock> _spawnedBlocks;
        private Vector2Int _foodSpawnInterval;
        private bool _isActive;

        private void Awake() => Instance = this;

        private void OnEnable()
        {
            GameManager.SystemStateChanged += OnSystemStateChanged;
            SnakeMovementController.BeforeSnakeMoved += OnBeforeSnakeMoved;
        }

        private void OnDisable()
        {
            GameManager.SystemStateChanged -= OnSystemStateChanged;
            SnakeMovementController.BeforeSnakeMoved -= OnBeforeSnakeMoved;
            StopCoroutine(StartGeneration());
        }

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
        }

        void Start()
        {
            Assert.IsNotNull(foodBlockPrefab);
            Assert.IsNotNull(OccupancyHandler.Instance);
            _occupancyHandler = OccupancyHandler.Instance;
            _spawnedBlocks = new Dictionary<Vector2Int, FoodBlock>();
            _foodSpawnInterval = DataManager.Instance.GameData.foodSpawnInterval;
        }

        public bool IsActive => _isActive;

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
            StartCoroutine(StartGeneration());
            Debug.Log("FoodManager.StartSystem");
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            StopCoroutine(StartGeneration());
            Debug.Log("FoodManager.StopSystem");
        }

        public void ClearSystem()
        {
            if (_spawnedBlocks.Count <= 0) return;
            Dictionary<Vector2Int, FoodBlock>.KeyCollection keys = _spawnedBlocks.Keys;
            foreach (Vector2Int pos in keys) _spawnedBlocks[pos].Despawn();
            _spawnedBlocks.Clear();
            Debug.Log("FoodManager.ClearSystem");
        }

        IEnumerator StartGeneration()
        {
            if (!_isActive) yield return null;
            while (_isActive)
            {
                yield return new WaitForSeconds(Random.Range(_foodSpawnInterval.x, _foodSpawnInterval.y));
                var position = Map.Instance.GetRandomCoordinate();
                SpawnBlock(position);
            }
        }

        void SpawnBlock(Vector2Int position)
        {
            if (!_isActive) return;
            if (_occupancyHandler.GetOccupancy(position) == BlockType.None)
            {
                var block = Instantiate(foodBlockPrefab) as FoodBlock;
                block.transform.parent = transform;
                block.transform.localPosition = Map.Instance.ToWorldPosition(position);
                _spawnedBlocks.Add(position, block);
                block.Spawn();
                _occupancyHandler.Occupy(position, block.Type);
            }
        }

        void OnBeforeSnakeMoved(Vector2Int position)
        {
            if(OccupancyHandler.Instance.IsOccupiedWith(position, BlockType.Food))
            {
                OccupancyHandler.Instance.UnOccupy(position);
                _spawnedBlocks[position].Despawn();
                _spawnedBlocks.Remove(position);
                GameStateHandler.Instance.AddScore();
            }
        }
    }
}
