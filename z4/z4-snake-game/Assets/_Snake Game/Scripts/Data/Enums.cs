using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Scripts.Data
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

    public enum LevelConclusion
    {
        Completed,
        Failed
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        None
    }

    public enum BlockType
    {
        None,
        Snake,
        Food,
        Hurdle
    }
}
