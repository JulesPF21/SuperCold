using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float frozeTime;
    private Vector3 m_Velocity;
    private bool isStatic;
    private Rigidbody rb;
    public float speed = 20f;
    private MeshRenderer meshRenderer;
    private void OnCollisionEnter(Collision other)
    {
        rb.isKinematic = true;
        meshRenderer.enabled = false;
        
        List<IFreezable> list = new List<IFreezable>();
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        for (var i = 0; i < colliders.Length; i++)
        {
            Collider c = colliders[i];
            
            IFreezable[] icable = c.gameObject.GetComponentsInChildren<IFreezable>();
            list.AddRange(icable);
        }
        

        foreach (IFreezable icable in list)
            StartCoroutine(Feezing(icable));
    }

    private IEnumerator Feezing(IFreezable icable)
    {
        float timer = 0f;
        icable.Freeze();
        
        while (timer < frozeTime)
        {
            timer += Time.unscaledDeltaTime;
            if (icable.RigidBody.isKinematic == false)
            {
                isStatic = false;
                icable.RigidBody.isKinematic = true;
            }
            yield return null;
        }

        if (isStatic == false)
        {
            icable.RigidBody.isKinematic = false;
        }
        icable.UnFreeze();
        Destroy(gameObject);
    }

    private void Awake()
    {
        Debug.Log("j'existe");
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        m_Velocity = transform.forward * speed;
    }
    
    void FixedUpdate()
    {
            
        if(rb.isKinematic)
            return;
            
        if (m_Velocity != Vector3.zero)
        {
            rb.linearVelocity = m_Velocity;
            m_Velocity = Vector3.zero; 
        }

        if (!TimeManager.isTimeFrozen)
        {
            rb.useGravity = true;
        }
    }
}

