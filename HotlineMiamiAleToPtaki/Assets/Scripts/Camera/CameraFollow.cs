using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player transform in the inspector
    public float followSpeed = 5f;
    public float cursorInfluence = 2f;
    public float cursorThreshold = 0.3f; // How close the cursor needs to be to the edge to move the camera

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Get the mouse position in viewport coordinates (0 to 1 range)
        Vector2 mouseViewportPos = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector2 cursorOffset = Vector2.zero;

        // Adjust offset based on cursor position
        if (mouseViewportPos.x < cursorThreshold) 
            cursorOffset.x = mouseViewportPos.x - cursorThreshold;
        else if (mouseViewportPos.x > 1 - cursorThreshold) 
            cursorOffset.x = mouseViewportPos.x - (1 - cursorThreshold);

        if (mouseViewportPos.y < cursorThreshold) 
            cursorOffset.y = mouseViewportPos.y - cursorThreshold;
        else if (mouseViewportPos.y > 1 - cursorThreshold) 
            cursorOffset.y = mouseViewportPos.y - (1 - cursorThreshold);

        cursorOffset *= cursorInfluence;

        // Target position is player's position + cursor influence
        Vector3 targetPosition = player.position + new Vector3(cursorOffset.x, cursorOffset.y, -10f);
        
        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
