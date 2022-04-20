using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cube.MiniGame.Systems;
using Cube.MiniGame.Data;

public class Test : MonoBehaviour
{
    public GameObject player;

    private void OnEnable() => InputManager.InputReceived += OnInputReceived;
    private void OnDisable() => InputManager.InputReceived -= OnInputReceived;

    void OnInputReceived(Direction direction)
    {
        Debug.Log($"Direction {direction}");
        if (direction == Direction.Left) player.transform.position += Vector3.left * Time.deltaTime * 10f;
        else if (direction == Direction.Right) player.transform.position += Vector3.right * Time.deltaTime * 10f;

    }
}
