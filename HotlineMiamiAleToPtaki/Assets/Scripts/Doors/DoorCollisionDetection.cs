using UnityEngine;

public class DoorCollisionDetection : MonoBehaviour
{
    SwingDoor swingDoor;

    private void Start()
    {
        swingDoor = GetComponentInParent<SwingDoor>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            swingDoor.PlayerDetected(other);
        }
    }
}
