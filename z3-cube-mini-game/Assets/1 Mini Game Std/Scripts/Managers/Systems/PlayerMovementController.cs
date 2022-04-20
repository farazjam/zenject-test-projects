using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cube.MiniGame.Data;
using System;

namespace Cube.MiniGame.Systems
{
    [DefaultExecutionOrder((int)ExecutionOrder.System)]
    public class PlayerMovementController : MonoBehaviour//, IGameSystem
    {
        private bool _isActive;
        [SerializeField] GameObject player;

        void Start()
        {
            Assert.IsNotNull(player);
            _isActive = true;
        }

        public bool IsActive => _isActive;

        private void OnEnable() => InputManager.InputReceived += Move;
        private void OnDisable() => InputManager.InputReceived -= Move;

        void Move(Direction direction)
        {
            if (direction == Direction.Left) player.transform.position += Vector3.left * Time.deltaTime * 10f;
            else if (direction == Direction.Right) player.transform.position += Vector3.right * Time.deltaTime * 10f;
        }

        /* public void StartSystem()
         {
             if (_isActive) return;
             _isActive = true;
             headBlock.Spawn();
             StartCoroutine(MovementLoop());
             Debug.Log("SnakeMovementController.StartSystem");
         }

         public void StopSystem()
         {
             if (!_isActive) return;
             _isActive = false;
             StopCoroutine(MovementLoop());
             Debug.Log("SnakeMovementController.StopSystem");
         }

         public void ClearSystem()
         {
             headBlock.Despawn();
             Debug.Log("SnakeMovementController.ClearSystem");
         }*/

        /*IEnumerator MovementLoop()
        {
            if (!_isActive) yield return null;
            while (_isActive)
            {
                var nextPosition = Map.Instance.GetNextCoordinate(headBlock.Coordinate, InputManager.Instance.Direction);
                BeforeSnakeMoved?.Invoke(nextPosition);
                headBlock.Move(nextPosition);
                AfterSnakeMoved?.Invoke(nextPosition);
                yield return new WaitForSeconds(_secondsPerTile);
            }
        }*/
    }
}
