using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject heldObject;
    public float radius = 2f;
    public float distance = 2f;
    public float height = 1f;

    private void Update()
    {
        var t = transform;
        var pressedE = Input.GetKeyDown(KeyCode.E);
        if (heldObject)
        {
            if (pressedE)
            {

            }
            var rigidbody = heldObject.GetComponent<Rigidbody>();
            var moveTo = t.position + distance * t.forward + height * t.up;
            var difference = moveTo - heldObject.transform.position;
            rigidbody.AddForce(difference * 500);
            heldObject.transform.rotation = t.rotation;
            if (pressedE)
            {
                rigidbody.drag = 1f;
                rigidbody.useGravity = true;
                heldObject = null;
            }
        }
        else
        {
            if (pressedE)
            {
                var hits = Physics.SphereCastAll(t.position + t.forward, radius, t.forward, radius);
                var hitIndex = Array.FindIndex(hits, hit => hit.transform.tag == "Pickupable");

                if (hitIndex != -1)
                {
                    var hitObject = hits[hitIndex].transform.gameObject;
                    heldObject = hitObject;
                    var rigidbody = heldObject.GetComponent<Rigidbody>();
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                    rigidbody.drag = 25f;
                    rigidbody.useGravity = false;
                }
;
            }
        }
    }
}