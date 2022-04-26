using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Data;
using Cube.MiniGame.Abstract;
using Cube.MiniGame.Blocks;
using Random = UnityEngine.Random;
using Zenject;

namespace Cube.MiniGame.Utils
{
    public interface IUtil 
    {
        Vector3 GetRandomPosition(BlockType type);
    }

    public class Util : IUtil
    {
        CubeGameData _data;
        int _coinSpawnRangeX;
        int _coinSpawnPositionY;
        int _hurdleSpawnRangeX;
        int _hurdleSpawnPositionY;

        public Util(CubeGameData data)
        {
            _data = data;
            _coinSpawnRangeX = _data.coin.SpawnRangeX;
            _coinSpawnPositionY = _data.coin.SpawnPositionY;
            _hurdleSpawnRangeX = _data.hurdle.SpawnRangeX;
            _hurdleSpawnPositionY = _data.hurdle.SpawnPositionY;
        }

        public Vector3 GetRandomPosition(BlockType type)
        {
            Vector3 result = Vector3.zero;
            if(type == BlockType.Coin) result = new Vector3(Random.Range(-_coinSpawnRangeX, _coinSpawnRangeX), _coinSpawnPositionY, 0);
            else if (type == BlockType.Hurdle) result = new Vector3(Random.Range(-_hurdleSpawnRangeX, _hurdleSpawnRangeX), _hurdleSpawnPositionY, 0);
            return result;
        }
    }
}
