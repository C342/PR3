using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticSlidingDoor : MonoBehaviour
{
    public Transform doorTransform;  // Assign the door GameObject's transform
    public Vector3 openOffset = new Vector3(0, 0, 3);  // How far the door moves when opening
    public float speed = 2f;  // Speed of movement

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;

    private void Start()
    {
        closedPosition = doorTransform.position;
        openPosition = closedPosition + openOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure the player has the "Player" tag
        {
            isOpening = true;
            StopAllCoroutines();
            StartCoroutine(MoveDoor(openPosition));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpening = false;
            StopAllCoroutines();
            StartCoroutine(MoveDoor(closedPosition));
        }
    }

    private System.Collections.IEnumerator MoveDoor(Vector3 target)
    {
        while (Vector3.Distance(doorTransform.position, target) > 0.01f)
        {
            doorTransform.position = Vector3.Lerp(doorTransform.position, target, Time.deltaTime * speed);
            yield return null;
        }
        doorTransform.position = target; // Ensure exact position is reached
    }
}
