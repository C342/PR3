using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Assuming Y=0 is the ground plane

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 mouseWorldPos = ray.GetPoint(enter); // Get the point where the ray hits the ground plane
                Vector3 playerPos = transform.position;

                Debug.DrawLine(playerPos, mouseWorldPos, Color.red);

                Vector3 directionToMouse = mouseWorldPos - playerPos;
                directionToMouse.y = 0f; // Keep the rotation on the horizontal plane
                if (directionToMouse.sqrMagnitude > 0f)
                {
                    transform.rotation = Quaternion.LookRotation(directionToMouse);
                }
            }
        }
    }
}