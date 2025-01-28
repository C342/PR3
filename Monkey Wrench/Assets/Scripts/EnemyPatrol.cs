using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;  // Array of patrol points
    public float patrolSpeed = 2f;    // Speed of patrolling
    public float waitTimeAtPoint = 2f; // Time to wait at each patrol point

    private int currentPointIndex = 0; // Current patrol point index
    private bool isWaiting = false; // Flag to check if the NPC is waiting at a point

    private void Update()
    {
        // If the NPC is not waiting, move towards the current patrol point
        if (!isWaiting)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // If there are no patrol points, exit
        if (patrolPoints.Length == 0)
            return;

        // Move towards the current patrol point
        Transform targetPoint = patrolPoints[currentPointIndex];
        float step = patrolSpeed * Time.deltaTime;

        // Move NPC to the patrol point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

        // If NPC reaches the patrol point, start waiting
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Start waiting at the patrol point
            isWaiting = true;
            Invoke("ContinuePatrolling", waitTimeAtPoint); // Wait at the point for a while
        }
    }

    private void ContinuePatrolling()
    {
        // Move to the next patrol point
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        isWaiting = false; // Stop waiting and continue patrolling
    }
}
