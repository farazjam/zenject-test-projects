using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Blocks;
using UnityEngine.Assertions;
using Snake.Scripts.Data;
using Snake.Scripts.Abstract;
using Random = UnityEngine.Random;

namespace Snake.Scripts.Systems
{
    public class HurdleGenerator : MonoBehaviour, IGameSystem
    {
        public static HurdleGenerator Instance;
        private OccupancyHandler _occupancyHandler;
        [SerializeField] private HurdleBlock hurdleBlockPrefab;
        public Dictionary<Vector2Int, HurdleBlock> _spawnedBlocks;
        private Vector2Int _hurdleSpawnInterval;
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

        public bool IsActive => _isActive;

        void Start()
        {
            Assert.IsNotNull(hurdleBlockPrefab);
            _occupancyHandler = OccupancyHandler.Instance;
            Assert.IsNotNull(_occupancyHandler);
            _spawnedBlocks = new Dictionary<Vector2Int, HurdleBlock>();
            _hurdleSpawnInterval = DataManager.Instance.GameData.hurdleSpawnInterval;
        }

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
            StartCoroutine(StartGeneration());
            Debug.Log("HurdleManager.StartSystem");
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            StopCoroutine(StartGeneration());
            Debug.Log("HurdleManager.StopSystem");
        }

        public void ClearSystem()
        {
            if (_spawnedBlocks.Count <= 0) return;
            Dictionary<Vector2Int, HurdleBlock>.KeyCollection keys = _spawnedBlocks.Keys;
            foreach (Vector2Int pos in keys) _spawnedBlocks[pos].Despawn();
            _spawnedBlocks.Clear();
            Debug.Log("HurdleManager.ClearSystem");
        }

        IEnumerator StartGeneration()
        {
            if (!_isActive) yield return null;
            while (_isActive)
            {
                yield return new WaitForSeconds(Random.Range(_hurdleSpawnInterval.x, _hurdleSpawnInterval.y));
                var position = Map.Instance.GetRandomCoordinate();
                SpawnBlock(position);
            }
        }

        void SpawnBlock(Vector2Int position)
        {
            if (!_isActive) return;
            if (_occupancyHandler.GetOccupancy(position) == BlockType.None)
            {
                var block = Instantiate(hurdleBlockPrefab) as HurdleBlock;
                block.transform.parent = transform;
                block.transform.localPosition = Map.Instance.ToWorldPosition(position);
                _spawnedBlocks.Add(position, block);
                block.Spawn();
                _occupancyHandler.Occupy(position, block.Type);
            }
        }

        void OnBeforeSnakeMoved(Vector2Int position)
        {
            if (OccupancyHandler.Instance.IsOccupiedWith(position, BlockType.Hurdle))
                GameStateHandler.Instance.LevelFailed();

        }
    }
}
