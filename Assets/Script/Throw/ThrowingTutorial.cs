using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.FPS.Gameplay;
using UnityEngine.InputSystem;

public class ThrowingTutorial : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow1;
    public GameObject objectToThrow2;
    private bool Proj;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;
    private InputAction throwAction1;
    private InputAction throwAction2;
    [SerializeField]
    private PlayerInput playerInput;
   
    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
        throwAction1 = playerInput.actions.FindAction("ThrowAbility1");
        throwAction2 = playerInput.actions.FindAction("ThrowAbility2");
    }

    private void OnEnable()
    {
        playerInput.actions.Enable();
    }

    private void Update()
    {
        if(throwAction1.IsPressed() && readyToThrow && totalThrows > 0)
        {
            Throw(objectToThrow1);
        }

        if (throwAction2.IsPressed() && readyToThrow && totalThrows > 0)
        {
            Throw(objectToThrow2);
        }
    }

    private void Throw(GameObject objectToThrow)
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}