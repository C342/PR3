using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour
{
    public GameObject door; // Assign your animated door in the Inspector
    public string requiredTag = "Pickupable"; // Set the tag for valid objects
    private int objectCount = 0;
    private const int requiredObjects = 3;
    private Animator doorAnimator;

    private void Start()
    {
        if (door != null)
            doorAnimator = door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag)) // Check if object has the required tag
        {
            objectCount++;
            Destroy(other.gameObject); // Destroy the object when placed
            CheckPuzzleCompletion();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            objectCount--;

            // Ensure count doesn't go negative
            objectCount = Mathf.Max(objectCount, 0);

            CheckPuzzleCompletion();
        }
    }

    private void CheckPuzzleCompletion()
    {
        if (doorAnimator != null)
        {
            if (objectCount >= requiredObjects)
            {
                doorAnimator.SetTrigger("OpenDoor");
            }
            else
            {
                doorAnimator.SetTrigger("CloseDoor");
            }
        }
    }
}