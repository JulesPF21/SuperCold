using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float radius;
    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private void OnCollisionEnter(Collision other)
    {
        rb.isKinematic = true;
        meshRenderer.enabled = false;
        
        List<IFlammable> list = new List<IFlammable>();
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        for (var i = 0; i < colliders.Length; i++)
        {
            Collider c = colliders[i];
            
            IFlammable[] flammables = c.gameObject.GetComponentsInChildren<IFlammable>();
            list.AddRange(flammables);
        }
        
        foreach (IFlammable flammable in list)
            if (flammable.canBurn == true)
            {
                StartCoroutine(SetFireTo(flammable));
            }
    }

    private IEnumerator SetFireTo(IFlammable flammable)
    {
        float timer = 0f;
        flammable.StartBurning();
        
        while (timer < flammable.TimeToBurn)
        {
            timer += Time.deltaTime;
            flammable.SetBurnedProgress(timer / flammable.TimeToBurn);
            yield return null;
        }
        
        flammable.StopBurning();
        Destroy(gameObject);
    }

    private void Awake()
    {
        Debug.Log("j'existe");
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
}
