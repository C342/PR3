using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour
{
    public string targetTag = "Pickupable";
    public float detectionRadius = 2f;
    public enum DoorEvents
    {
        None,
        Playerdetected
    }

    private int destroyedCount = 0;

    void Update()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider col in nearbyObjects)
        {
            if (col.CompareTag(targetTag))
            {
                Destroy(col.gameObject);

                destroyedCount++;

                if (destroyedCount >= 3)
                {
                    OpenDoor();
                }
            }
        }
    }
    public enum DoorEvents
    {
        None,
        Playerdetected
    }

    private DoorEvents events = DoorEvents.None;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            events = DoorEvents.PlayerDetected;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            events = DoorEvents.None;
        }
    }

    public DoorEvents Events
    {
        get { return events; }
    }

}