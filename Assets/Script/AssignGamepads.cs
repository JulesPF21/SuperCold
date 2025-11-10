using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssignGamepads : MonoBehaviour
{
    public int playerIndex; // 0 ou 1
    private Gamepad gamepad;

    [SerializeField]
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        if (Gamepad.all.Count > playerIndex)
        {
            gamepad = Gamepad.all[playerIndex];
            Debug.Log($"Player {playerIndex+1} utilise {gamepad.displayName}");
        }
        else
        {
            Debug.LogWarning($"Pas de manette pour Player {playerIndex+1}");
        }
    }

    void Update()
    {
        if (gamepad == null) return;

        Vector2 move = gamepad.leftStick.ReadValue();
        transform.Translate(move * 5f * Time.deltaTime);
    }
}