using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Cube.MiniGame.Data;
using Cube.MiniGame.Blocks;
using Cube.MiniGame.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using Zenject;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class CoinSpawner : IInitializable, IDisposable, IGameSystem
    {
        private bool _isActive;
        private CoinBlock.CoinBlockPool _pool;
        private SignalBus _signalBus;
        private CubeGameData _data;
        private Vector2Int _spawnInterval;
        private CancellationTokenSource _cts;

        public bool IsActive => _isActive;

        public CoinSpawner(CoinBlock.CoinBlockPool coinBlockPool, SignalBus signalBus, CubeGameData data)
        {
            _pool = coinBlockPool;
            _signalBus = signalBus;
            _data = data;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SystemStateChangedSignal>(OnSystemStateChanged);
            _signalBus.Subscribe<BlockTouchedSignal>(Despawn);
            _spawnInterval = _data.coin.SpawnInterval;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SystemStateChangedSignal>(OnSystemStateChanged);
            _signalBus.Unsubscribe<BlockTouchedSignal>(Despawn);
        }

        public void OnSystemStateChanged(SystemStateChangedSignal args)
        {
            SystemState state = args.state;
            if (state == SystemState.Start) StartSystem();
            else if (state == SystemState.Stop) StopSystem();
            else if (state == SystemState.Clear) ClearSystem();
            Debug.Log($"CoinGenerator System :{state}");
        }

        public void StartSystem()
        {
            if (_isActive) return;
            _isActive = true;
            StartGeneration();
        }

        public void StopSystem()
        {
            if (!_isActive) return;
            _isActive = false;
            StopGeneration();
        }

        public void ClearSystem() { }

        public void StartGeneration()
        {
            if (!_isActive) return;
            _cts = new CancellationTokenSource();
            GenerationLoop();
        }

        public void StopGeneration()
        {
            _cts.Cancel();
            _cts = null;
        }

        private async UniTask GenerationLoop()
        {
            while (true)
            {
                var randomTime = Random.Range(_spawnInterval.x, _spawnInterval.y);
                var isCancelled = await UniTask.Delay(TimeSpan.FromSeconds(randomTime), cancellationToken: _cts.Token).SuppressCancellationThrow();
                if (isCancelled) return;
                Spawn();
            }
        }

        private void Spawn()
        {
            if (!_isActive) return;
            _pool.Spawn();
        }

        public void Despawn(BlockTouchedSignal args)
        {
            if (args.toucherType == BlockType.Coin) _pool.Despawn((CoinBlock)args.toucherBlock);
        }
    }
}