using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Keep Z position the same (for 2D setups)

        // Calculate the direction to the cursor
        Vector3 direction = mousePosition - transform.position;

        // Rotate the object to look at the cursor, ensuring rotation only around Z-axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
