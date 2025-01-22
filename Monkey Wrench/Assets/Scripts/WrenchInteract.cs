using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UIElements;
using UnityEngine.EventSystems;

public class WrenchInteract : MonoBehaviour
{
    public GameObject WrenchPickedUp;
    public GameObject WrenchIcon;

    void Start()
    {
        WrenchPickedUp.SetActive(false);
        WrenchIcon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WrenchIcon.SetActive(true);

            this.gameObject.SetActive(false);

            WrenchPickedUp.SetActive(true);
        }

        if (other.gameObject.tag == null)
        {
            WrenchIcon.SetActive(false);
        }
    }
}