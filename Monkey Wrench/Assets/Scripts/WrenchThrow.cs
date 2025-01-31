using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class WrenchThrow : MonoBehaviour
{
    public float throwForce = 10f;  // Throwing force
    public GameObject objectToThrow; // The object being thrown
    public Camera playerCamera; // Player's camera
    public bool hasThrown = false; // Prevent multiple throws

    private Rigidbody rb;

    private void Start()
    {
        if (objectToThrow != null)
            rb = objectToThrow.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !hasThrown)
        {
            ThrowObjectAtCursor();
        }
    }

    private void ThrowObjectAtCursor()
    {
        if (objectToThrow != null && playerCamera != null && rb != null)
        {
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 throwDirection = (hit.point - objectToThrow.transform.position).normalized;
                throwDirection.y = 0; // Keep the throw mostly horizontal (optional)

                rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
                hasThrown = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasThrown && rb != null)
        {
            if (collision.gameObject.CompareTag("Wall")) // Ensure it's a wall
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;

                // Optionally attach to the surface
                objectToThrow.transform.SetParent(collision.transform);
            }
        }
    }

    public void ResetThrow()
    {
        hasThrown = false;
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            objectToThrow.transform.SetParent(null);
            objectToThrow.transform.position = transform.position;
            objectToThrow.transform.rotation = Quaternion.identity;
        }
    }
}