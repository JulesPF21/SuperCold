using UnityEngine;

public class FreezeableProjectile : MonoBehaviour
{
    private Rigidbody rb;
    private bool isFrozen = false;
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (TimeManager.isTimeFrozen && !isFrozen)
        {
            Freeze();
        }
        else if (!TimeManager.isTimeFrozen && isFrozen)
        {
            Unfreeze();
        }
    }

    public void Freeze()
    {
        if (isFrozen) return;
        isFrozen = true;

        if (rb != null)
        {
            // On enregistre la vitesse et on stoppe tout
            savedVelocity = rb.linearVelocity;
            savedAngularVelocity = rb.angularVelocity;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.isKinematic = true;
        }
    }

    public void Unfreeze()
    {
        if (!isFrozen) return;
        isFrozen = false;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            rb.linearVelocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }
    }
}