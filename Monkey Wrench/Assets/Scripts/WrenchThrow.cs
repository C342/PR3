using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

public class WrenchThrow : MonoBehaviour
{
    public float throwForce = 10f;  // The force with which the object is thrown
    public GameObject objectToThrow; // The object to be thrown (usually an object with a Rigidbody)
    public Camera playerCamera; // Reference to the camera (player's perspective)

    public bool hasThrown = false; // Flag to prevent re-throwing the object

    private void Update()
    {
        // Check for throw input (e.g., pressing the left mouse button or any key you prefer)
        if (Input.GetButtonDown("Fire1") && !hasThrown)
        {
            ThrowObjectAtCursor();
        }
    }

    private void ThrowObjectAtCursor()
    {
        if (objectToThrow != null && playerCamera != null)
        {
            // Get the Rigidbody of the object to be thrown
            Rigidbody rb = objectToThrow.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Ensure continuous collision detection to prevent tunneling
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                // Raycast from the camera to the cursor position in world space
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Calculate direction from the object to the hit point (where the cursor is)
                    Vector3 throwDirection = hit.point - objectToThrow.transform.position;

                    // Normalize the direction to avoid too much force in certain axes
                    throwDirection.y = 0; // Optional: to keep the throw on a horizontal plane (remove if you want 3D throws)
                    throwDirection.Normalize();

                    // Apply force to throw the object in the direction of the cursor
                    rb.isKinematic = false; // Enable physics for throwing
                    rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

                    hasThrown = true; // Set the flag so the object cannot be thrown again
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the object hits a surface, make it stick by freezing its position and rotation
        if (hasThrown)
        {
            Rigidbody rb = objectToThrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Stop the object's motion when it hits a wall or other solid object
                rb.isKinematic = true; // Disable physics after the object sticks
                rb.velocity = Vector3.zero; // Stop any motion
                rb.angularVelocity = Vector3.zero; // Stop any spinning

                // Parent the object to the surface it collided with
                rb.transform.SetParent(collision.transform); // Make the object stick to the surface

                // Optionally, disable the collider to prevent further collisions
                Collider col = objectToThrow.GetComponent<Collider>();
                if (col != null)
                {
                    col.enabled = false; // Disable the collider to prevent further collisions after sticking
                }
            }
        }
    }

    // Optional: Reset the object for re-throwing (you can call this manually)
    public void ResetThrow()
    {
        hasThrown = false;
        Rigidbody rb = objectToThrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Disable kinematic to allow physics again
            rb.velocity = Vector3.zero; // Reset velocity
            rb.angularVelocity = Vector3.zero; // Reset angular velocity
            rb.transform.SetParent(null); // Detach the object from the wall
            objectToThrow.transform.position = transform.position; // Optionally reset position
            objectToThrow.transform.rotation = Quaternion.identity; // Optionally reset rotation

            // Re-enable the collider so the object can collide again
            Collider col = objectToThrow.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
            }
        }
    }
}