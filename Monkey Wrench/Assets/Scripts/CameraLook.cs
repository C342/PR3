using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform player;        // Reference to the player
    public float moveSpeed = 10f;   // Speed at which the camera moves
    public float maxDistance = 5f; // Maximum distance the camera can move from the player

    private Camera mainCamera;      // Reference to the main camera
    private bool isDragging = false; // Tracks if RMB is held

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera if not manually assigned
    }

    void Update()
    {
        // Check for right mouse button input
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // Move the camera only when RMB is held
        if (isDragging)
        {
            MoveCameraWithMouse();
        }
        else
        {
            // When not dragging, keep the camera centered on the player
            CenterOnPlayer();
        }
    }

    void MoveCameraWithMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(mainCamera.transform.position.y)));

        // Calculate direction from the player to the mouse position
        Vector3 direction = (mousePosition - player.position);
        direction.y = 0; // Ensure movement is only in the horizontal plane
        direction = direction.normalized;

        // Calculate the target position for the camera
        Vector3 targetPosition = player.position + direction * maxDistance;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), moveSpeed * Time.deltaTime);
    }

    void CenterOnPlayer()
    {
        // Smoothly center the camera on the player's position
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}