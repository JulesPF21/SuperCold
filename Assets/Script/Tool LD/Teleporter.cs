using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Transform exitPortal; // The portal to which the player will be teleported

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the object entering the portal is tagged as "Player"
        {
            Teleport(other.transform);
        }
    }

    private void Teleport(Transform player)
    {
        player.position = exitPortal.position; // Set the player's position to the exit portal's position
        player.rotation = exitPortal.rotation; // Optionally, match the player's rotation to the exit portal's rotation
    }
}