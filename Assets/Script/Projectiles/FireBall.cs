using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject VFXprefab;
    [SerializeField] private GameObject VFXground;
    private GameObject vfx;
    private GameObject groundVfx;
    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private void OnCollisionEnter(Collision other)
    {
        rb.isKinematic = true;
        meshRenderer.enabled = false;
        vfx = Instantiate(VFXprefab, transform.position, transform.rotation);
        RaycastHit GroundFire;
        if (Physics.Raycast(transform.position, Vector3.down, out GroundFire, radius))
        {
            groundVfx = Instantiate(VFXground, GroundFire.point, transform.rotation);
        }
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
        Destroy(vfx);
        Destroy(groundVfx);
        Destroy(gameObject);
    }

    private void Awake()
    {
        Debug.Log("j'existe");
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
}
