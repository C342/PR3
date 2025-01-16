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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                WrenchIcon.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                this.gameObject.SetActive(false);

                WrenchPickedUp.SetActive(true);
            }
        }

        if (other.gameObject.tag == null)
        {
            WrenchIcon.SetActive(false);
        }
    }
}