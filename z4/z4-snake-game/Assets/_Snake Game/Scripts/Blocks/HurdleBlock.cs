using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Scripts.Data;
using Snake.Scripts.Abstract;

namespace Snake.Scripts.Blocks
{
    public class HurdleBlock : Block
    {
        public override void Spawn()
        {
            _type = BlockType.Hurdle; 
        }
    }
}
