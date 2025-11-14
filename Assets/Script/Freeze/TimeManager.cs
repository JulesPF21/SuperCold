using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    [Header("Timing Settings")]
    public float freezeDuration = 5f;
    public float freeTime = 2f;
    public float freeMovementDuration = 1f; 
    public GameObject player1;
    public GameObject player2;
    public static bool isTimeFrozen = false;
    
    [Header("Player HUDs")]
    public GameObject player1HUD;
    public GameObject player2HUD;
    
    [Header("Player")]
    private GameObject currentPlayer;
    private GameObject otherPlayer;
    private List<FreezeableProjectile> frozenProjectiles = new List<FreezeableProjectile>();

    private enum Phase { PlayerTurn, FreeTurn, ProjectilePhase }
    private Phase currentPhase = Phase.PlayerTurn;
    
    
    private void Start()
    {
        Time.timeScale = 0.3f;
        StartTurn(player1, player2);
    }
    
    private void StartTurn(GameObject activePlayer, GameObject waitingPlayer)
    {
        currentPlayer = activePlayer;
        otherPlayer = waitingPlayer;
        StartCoroutine(TurnCoroutine());
    }
    
    private IEnumerator TurnCoroutine()
    {
        
        currentPhase = Phase.PlayerTurn;
        SetPlayerInput(otherPlayer, false);
        SetPlayerInput(currentPlayer, true);
        UpdateTurnIndicator(currentPhase);
        isTimeFrozen = true;

        yield return new WaitForSecondsRealtime(freezeDuration);

        currentPhase = Phase.FreeTurn;
        SetPlayerInput(otherPlayer, true);
        SetPlayerInput(currentPlayer, true);
        UpdateTurnIndicator(currentPhase);
        isTimeFrozen = true;
        
        yield return new WaitForSecondsRealtime(freeTime);
        
        currentPhase = Phase.ProjectilePhase;
        SetPlayerInput(otherPlayer, false);
        SetPlayerInput(currentPlayer, false);
        UpdateTurnIndicator(currentPhase);
        isTimeFrozen = false; 

        yield return new WaitForSecondsRealtime(freeMovementDuration);

        
        StartTurn(otherPlayer, currentPlayer);
    }
    private void SetPlayerInput(GameObject player, bool active)
    {
        player.GetComponent<PlayerCharacterController>().enabled = active;
        player.GetComponent<PlayerWeaponsManager>().enabled = active;
        
        
        var rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = !active;
        }
    }
    private void UpdateTurnIndicator(Phase phase)
    {
        switch (phase)
        {
            case Phase.PlayerTurn:
                if (player1HUD != null)
                    player1HUD.SetActive(currentPlayer == player1);
                if (player2HUD != null)
                    player2HUD.SetActive(currentPlayer == player2);
                break;

            case Phase.FreeTurn:
                if (player1HUD != null)
                    player1HUD.SetActive(true);
                if (player2HUD != null)
                    player2HUD.SetActive(true);
                break;

            case Phase.ProjectilePhase:
                if (player1HUD != null)
                    player1HUD.SetActive(false);
                if (player2HUD != null)
                    player2HUD.SetActive(false);
                break;
        }
    }
}
