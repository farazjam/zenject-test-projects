using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Data;
using Snake.Scripts.Systems;
using Snake.Scripts.Abstract;

namespace Snake.Scripts.Blocks
{
    public class FoodBlock : Block
    {
        public override void Spawn() => _type = BlockType.Food;
    }
}
