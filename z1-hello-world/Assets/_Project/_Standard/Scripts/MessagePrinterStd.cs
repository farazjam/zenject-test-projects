using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace standard
{
    public class MessagePrinterStd : MonoBehaviour
    {
        [SerializeField] Message message;

        private void Update() => Debug.Log($"Message1 {message.Msg}");
    }
}