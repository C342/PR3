using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

public class WrenchThrow : MonoBehaviour
{
    [Header("Throw Settings")]
    public GameObject throwablePrefab;
    public Transform throwOrigin;
    public float throwForce = 10f;
    public bool Thrown = true;

    [Header("Input Settings")]
    public KeyCode throwKey = KeyCode.Mouse0;

    private void Start()
    {
        Thrown = true;
    }
    void Update()
    {
        Debug.Log(Thrown);
        if (Thrown && Input.GetKeyDown(throwKey))
        {
            Debug.Log("first if ");
            ThrowObject();
        }
    }

    private void ThrowObject()
    {
        if (throwablePrefab == null || throwOrigin == null)
        {
            
            Debug.LogWarning("ThrowablePrefab or ThrowOrigin is not assigned.");
            
            Debug.Log("second if ");
            Thrown = false;
            return;
            
        }

        GameObject thrownObject = Instantiate(throwablePrefab, throwOrigin.position, throwOrigin.rotation);

        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = thrownObject.AddComponent<Rigidbody>();
        }

        rb.AddForce(throwOrigin.forward * throwForce, ForceMode.Impulse);
    }
}