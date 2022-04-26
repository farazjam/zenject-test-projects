using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube.MiniGame.Data
{
    public enum ExecutionOrder
    {
        BeforeSystem = 100,
        System = 200,
        AfterSystem = 200,
    }

    public enum SystemState
    {
        Start,
        Stop,
        Clear
    }

    public enum GameState
    {
        Idle,
        Gameplay,
        Gameover
    }

    public enum LevelConclusion
    {
        Completed,
        Failed
    }

    public enum Direction
    {
        Left,
        Right,
        None
    }

    public enum BlockType
    {
        None,
        Player,
        Coin,
        Hurdle
    }
}
