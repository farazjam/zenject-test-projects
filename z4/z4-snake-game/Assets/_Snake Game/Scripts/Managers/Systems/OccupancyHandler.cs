using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Data;
using Snake.Scripts.Abstract;
using System;
using UnityEngine.Assertions;

namespace Snake.Scripts.Systems
{
    public class OccupancyHandler : MonoBehaviour, IGameSystem
    {
        public static OccupancyHandler Instance;
        private Map _map;
        public Dictionary<Vector2Int, BlockType> _occupancy;
        private bool _isActive;

        private void Awake() => Instance = this;
        private void OnEnable() => GameManager.SystemStateChanged += OnSystemStateChanged;
        private void OnDisable() => GameManager.SystemStateChanged -= OnSystemStateChanged;
        public bool IsActive => _isActive;

        public void OnSystemStateChanged(SystemState state)
        {
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
        }

        private void Start()
        {
            Assert.IsNotNull(Map.Instance);
            _map = Map.Instance;
            _occupancy = new Dictionary<Vector2Int, BlockType>();
        }

        public void Occupy(Vector2Int coordinate, BlockType type)
        {
            if (!_isActive) return;
            if (!_map.IsCoordinateValid(coordinate)) return;
            if (!_occupancy.ContainsKey(coordinate)) _occupancy.Add(coordinate, type);
            _occupancy[coordinate] = type;
        }

        public void UnOccupy(Vector2Int coordinate)
        {
            if (!_isActive) return;
            if (!_map.IsCoordinateValid(coordinate)) return;
            if (_occupancy.ContainsKey(coordinate)) _occupancy.Remove(coordinate);
        }

        public BlockType GetOccupancy(Vector2Int coordinate)
        {
            return _occupancy.ContainsKey(coordinate) ? _occupancy[coordinate] : BlockType.None;
        }

        public bool IsOccupiedWith(Vector2Int coordinate, BlockType type)
        {
            return GetOccupancy(coordinate) == type;
        }

        public void ClearAllOccupancies() => _occupancy.Clear();

        public void StartSystem() 
        {
            if (_isActive) return;
            _isActive = true;
            Debug.Log("OccupancyHandler.StartSystem");
        }

        public void StopSystem() 
        {
            if (!_isActive) return;
            _isActive = false;
            Debug.Log("OccupancyHandler.StopSystem");
        }

        public void ClearSystem() 
        {
            ClearAllOccupancies();
            Debug.Log("OccupancyHandler.ClearSystem");
        }

       
    }
}
