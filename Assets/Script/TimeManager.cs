using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Timing Settings")]
    public float freezeDuration = 5f; 
    public float freeMovementDuration = 1f; 
    public GameObject player1;
    public GameObject player2;

    private GameObject currentPlayer;
    private GameObject otherPlayer;
    private List<FreezeableProjectile> frozenProjectiles = new List<FreezeableProjectile>();

    private void Start()
    {
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
        FreezePlayer(otherPlayer);  
        yield return new WaitForSecondsRealtime(freezeDuration);

        UnfreezePlayer(otherPlayer);
        yield return new WaitForSecondsRealtime(freeMovementDuration);

        StartTurn(otherPlayer, currentPlayer);
    }

    private void FreezePlayer(GameObject player)
    {
       
        foreach (var script in player.GetComponents<MonoBehaviour>())
        {
            if (script != this) 
                script.enabled = false;
        }
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; 
        }


        // Geler les projectiles
        frozenProjectiles.Clear();
        foreach (var proj in FindObjectsOfType<FreezeableProjectile>())
        {
            proj.Freeze();
            frozenProjectiles.Add(proj);
        }
    }

    private void UnfreezePlayer(GameObject player)
    {
        
        foreach (var script in player.GetComponents<MonoBehaviour>())
        {
            if (script != this)
                script.enabled = true;
        }
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; 
        }


        foreach (var proj in frozenProjectiles)
        {
            if (proj != null)
                proj.Unfreeze();
        }
        frozenProjectiles.Clear();
    }
}
