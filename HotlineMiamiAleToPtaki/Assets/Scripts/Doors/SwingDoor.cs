using UnityEngine;
using UnityEngine.UIElements;

public class SwingDoor : MonoBehaviour
{
    public Transform door; // Assign the door GameObject
    public float maxAngle = 90f; // Maximum rotation angle from start
    public float swingSpeed = 2f; // Speed of door swinging
    private DamageArea damageArea;

    private float currentAngle = 0f;
    private Quaternion originalRotation;
    private bool isMoving = false;
    private float targetAngle = 0f;

    private void Start()
    {
        originalRotation = door.rotation;
        damageArea = GetComponentInChildren<DamageArea>();
        damageArea.isDealingDamage = false;
    }

    public void PlayerDetected(Collider2D other)
    {
        if (isMoving)
        {
            return;
        }
        DetermineSwingDirection(other.transform.position);
        isMoving = true;
        damageArea.isDealingDamage = true;
    }

    private void DetermineSwingDirection(Vector3 playerPosition)
    {
        Vector2 doorRight = door.right; // Right direction in 2D
        Vector2 playerToDoor = (playerPosition - door.position).normalized;

        float dot = Vector2.Dot(doorRight, playerToDoor);
        float angleDirection = dot > 0 ? -1 : 1;

        targetAngle = Mathf.Clamp(currentAngle + (angleDirection * maxAngle), -maxAngle, maxAngle);
    }

    private void Update()
    {
        if (isMoving)
        {
            float step = swingSpeed * Time.deltaTime;
            currentAngle = Mathf.MoveTowards(currentAngle, targetAngle, step);

            door.rotation = originalRotation * Quaternion.Euler(0, 0, currentAngle);

            if (Mathf.Approximately(currentAngle, targetAngle))
            {
                isMoving = false;
                damageArea.isDealingDamage = false;
            }
        }
    }
}
