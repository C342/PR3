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
        Vector3 MouseScreenToCameraSpace = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
        Vector3 PlayerScreenToCameraSpace = new(Camera.main.WorldToScreenPoint(transform.position).x, 0f, Camera.main.WorldToScreenPoint(transform.position);
        //Debug.Log(Input.mousePosition);
        Vector3 PlayerToMouse = MouseScreenToCameraSpace - PlayerScreenToCameraSpace;
        Debug.DrawLine(transform.position, PlayerToMouse);
        transform.LookAt(PlayerToMouse);

    }
}