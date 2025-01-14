using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineToEmptyObject : MonoBehaviour
{
    public Transform targetObject; // The empty object to connect the LineRenderer to

    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();

        // Ensure the LineRenderer has at least two positions
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object is not assigned.");
            return;
        }

        // Check if the right mouse button is being held down
        if (Input.GetMouseButton(1)) // 1 corresponds to the right mouse button
        {
            // Set the start and end positions of the line
            lineRenderer.SetPosition(0, transform.position);      // Start position (current object)
            lineRenderer.SetPosition(1, targetObject.position);   // End position (target object)
        }
        else
        {
            // If the RMB is not held, hide the line
            lineRenderer.enabled = false;
        }

        // Ensure the line is visible when the RMB is held
        lineRenderer.enabled = Input.GetMouseButton(1);
    }
}