using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDetect : MonoBehaviour
{
    public Transform ground;
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == ground)
            {
                transform.position = hit.point;
            }

        }
    }
}