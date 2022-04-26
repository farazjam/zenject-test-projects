using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DI2
{
    public interface IMessage
    {
        public string Msg { get; }
    }

    public class MessageA : IMessage
    {
        private string _msg = "Hello World DI2";

        public string Msg => _msg;
    }

    public class MessageB : IMessage
    {
        private string _msg = "Good Bye DI2";

        public string Msg => _msg;
    }
}
