using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour
{
    public string targetTag = "Pickupable";  // Tag of the objects to destroy
    public float detectionRadius = 5f;       // Radius within which objects will be detected
    public Transform door;                   // The door that will slide open
    public Vector3 openPosition;             // The position the door will move to when it opens
    public float moveSpeed = 2f;             // Speed at which the door moves

    private int destroyedCount = 0;          // Count of destroyed objects
    private bool doorIsOpen = false;         // Flag to check if the door has already opened

    void Update()
    {
        // Find all colliders within the detection radius
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider col in nearbyObjects)
        {
            // Check if the object has the specified tag
            if (col.CompareTag(targetTag))
            {
                // Destroy the object
                Destroy(col.gameObject);

                // Increment the destroyed object counter
                destroyedCount++;

                // Check if 3 objects have been destroyed and the door hasn't opened yet
                if (destroyedCount >= 3 && !doorIsOpen)
                {
                    doorIsOpen = true;  // Set the flag to true so the door won't open again
                    StartCoroutine(OpenDoorCoroutine());  // Start the door opening animation
                }
            }
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        // Gradually move the door to the open position
        while (Vector3.Distance(door.position, openPosition) > 0.1f) // Set a small margin to avoid overshooting
        {
            door.position = Vector3.MoveTowards(door.position, openPosition, moveSpeed * Time.deltaTime);
            yield return null;  // Wait for the next frame
        }

        // Ensure the door reaches the exact open position
        door.position = openPosition;
    }
}