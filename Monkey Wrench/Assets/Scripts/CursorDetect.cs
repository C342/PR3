using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDetect : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public Transform player;
    public float maxDistanceFromPlayer = 5.0f;

    private Vector3 offset;
    private bool isRMBPressed = false;

    void Start()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            isRMBPressed = true;
            FollowCursor();
        }
        else
        {
            isRMBPressed = false;
        }
    }

    void FollowCursor()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorWorldPosition.z = transform.position.z;  // Keep the camera's Z position constant

        Vector3 targetPosition = cursorWorldPosition + offset;
        Vector3 playerPosition = player.position;

        Vector3 directionToPlayer = targetPosition - playerPosition;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > maxDistanceFromPlayer)
        {
            targetPosition = playerPosition + directionToPlayer.normalized * maxDistanceFromPlayer;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, sensitivity * Time.deltaTime);
    }
}