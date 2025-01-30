using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;         // Assign the player in the Inspector
    public float detectionRange = 10f;  // Radius for detecting the player
    public float chaseSpeed = 4f;       // Normal speed
    public float slowSpeed = 1.5f;      // Slowed speed when hit
    public float attackDistance = 1.5f; // Distance to "kill" the player
    public float slowDuration = 3f;     // How long the enemy stays slowed

    private bool isChasing = false;
    private NavMeshAgent agent;
    private float originalSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            originalSpeed = chaseSpeed;
            agent.speed = chaseSpeed;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (agent != null)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        Debug.Log("Player has been caught!");
        Destroy(player.gameObject); // Replace with actual game-over logic
    }

    // Slow down enemy when hit by a specific object (e.g., a projectile)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wrench")) // Make sure the slowing object has this tag
        {
            Debug.Log("Enemy hit! Slowing down...");
            SlowEnemy();
        }
    }

    void SlowEnemy()
    {
        chaseSpeed = slowSpeed;
        if (agent != null) agent.speed = slowSpeed;
        Invoke(nameof(ResetSpeed), slowDuration);
    }

    void ResetSpeed()
    {
        chaseSpeed = originalSpeed;
        if (agent != null) agent.speed = originalSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}