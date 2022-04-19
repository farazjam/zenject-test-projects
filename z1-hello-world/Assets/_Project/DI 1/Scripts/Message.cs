using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DI1 
{
    public interface IMessage
    {
        public string Msg { get; }
    }

    public class Message : IMessage
    {
        private string _msg = "Hello World DI1";

        public string Msg => _msg;
    }
}


