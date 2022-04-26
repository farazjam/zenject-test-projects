using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DI2
{
    public class MessagePrinterDI2 : MonoBehaviour
    {
        [Inject] IMessage message1;
        [Inject] IMessage message2;

        private void Update() => Debug.Log($"Message1 {message1.Msg}, Message2 {message2.Msg}");
    }
}
