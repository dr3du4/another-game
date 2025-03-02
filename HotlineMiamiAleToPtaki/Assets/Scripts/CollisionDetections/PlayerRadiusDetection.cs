using UnityEngine;

public class PlayerRadiusDetection : MonoBehaviour
{
    Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 direction = other.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, LayerMask.GetMask("Wall")); 
            
            // If no wall is detected, allow detection
            if (!hit.collider)
            {
                enemy.OnPlayerEnteredDetectionRadius(other.transform);
            }
        }
    }

    public void UpdateDetectionRadius(float detectionRadius)
    {
        GetComponent<CircleCollider2D>().radius = detectionRadius;
    }
}
