using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace standard
{
   /* public abstract class IMessage
    {
        public string Msg { get; }
    }*/

    [Serializable]
    public class Message //: IMessage
    {
        [SerializeField] private string _msg = "Hello World Std";

        public string Msg => _msg;
    }
}
