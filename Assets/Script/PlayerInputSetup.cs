using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSetup : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField] private int playerIndex;
    private void Start()
    {
        var manager = FindFirstObjectByType<PlayerInputManager>(FindObjectsInactive.Exclude);
        //manager.JoinPlayer(playerIndex);
    }
}
