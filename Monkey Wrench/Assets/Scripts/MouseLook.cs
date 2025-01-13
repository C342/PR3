using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector3 playerPos = transform.position;

            Debug.DrawLine(playerPos, mouseWorldPos, Color.red);

            Vector3 directionToMouse = mouseWorldPos - playerPos;
            directionToMouse.y = 0f;
            if (directionToMouse.sqrMagnitude > 0f)
            {
                transform.rotation = Quaternion.LookRotation(directionToMouse);
            }
        }
    }
}