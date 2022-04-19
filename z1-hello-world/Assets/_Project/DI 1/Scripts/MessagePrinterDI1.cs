using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DI1
{
    public class MessagePrinterDI1 : MonoBehaviour
    {
        [Inject] IMessage message;

        private void Update() => Debug.Log($"Message {message.Msg}");
    }
}
