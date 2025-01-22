using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class DoorTrigger : MonoBehaviour
{

    public enum DoorEvents
    {
        None,
        PlayerDetected,
    };

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