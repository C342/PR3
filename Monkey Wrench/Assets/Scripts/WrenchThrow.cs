using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

public class WrenchThrow : MonoBehaviour
{
    public Transform camera;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int TotalThrows;
    public float throwCooldown;

    [Header("Settings")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool ReadyToThrow;

    private void Start()
    {
        ReadyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && ReadyToThrow && TotalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        ReadyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, camera.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = camera.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        TotalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        ReadyToThrow = true;
    }
}