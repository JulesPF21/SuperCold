using UnityEngine;

public class FreezeableProjectile : MonoBehaviour
{
<<<<<<< HEAD
    private Rigidbody rb;
    private bool isFrozen = false;
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

    private void Awake()
=======
    public Rigidbody rb;
    private Vector3 savedVelocity;
    private bool isFrozen = false;

    void Awake()
>>>>>>> Quentin
    {
        rb = GetComponent<Rigidbody>();
    }

<<<<<<< HEAD
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
=======
    void FixedUpdate()
    {
        if (isFrozen)
            rb.linearVelocity = Vector3.zero;
        else
            savedVelocity = rb.linearVelocity;
>>>>>>> Quentin
    }

    public void Freeze()
    {
<<<<<<< HEAD
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
=======
        isFrozen = true;
        rb.isKinematic = true;
>>>>>>> Quentin
    }

    public void Unfreeze()
    {
<<<<<<< HEAD
        if (!isFrozen) return;
        isFrozen = false;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            rb.linearVelocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }
=======
        isFrozen = false;
        rb.isKinematic = false;
        rb.linearVelocity = savedVelocity;
>>>>>>> Quentin
    }
}