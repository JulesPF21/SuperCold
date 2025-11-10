using UnityEngine;

public class FreezeableProjectile : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 savedVelocity;
    private bool isFrozen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isFrozen)
            rb.linearVelocity = Vector3.zero;
        else
            savedVelocity = rb.linearVelocity;
    }

    public void Freeze()
    {
        isFrozen = true;
        rb.isKinematic = true;
    }

    public void Unfreeze()
    {
        isFrozen = false;
        rb.isKinematic = false;
        rb.linearVelocity = savedVelocity;
    }
}