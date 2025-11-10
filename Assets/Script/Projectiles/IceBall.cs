using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using Unity.VisualScripting;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float frozeTime;
    
    private Rigidbody rb;
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
            timer += Time.deltaTime;
            
            yield return null;
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
}

