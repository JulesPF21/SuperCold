using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Transform exitPortal; // The portal to which the player will be teleported

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the object entering the portal is tagged as "Player"
        {
            player = other.gameObject;
            player.SetActive(false);
            player.transform.position = exitPortal.position;
            player.transform.rotation = exitPortal.rotation;
            player.SetActive(true);
        }
    }
    
}