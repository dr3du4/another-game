using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class RatDetectionRadius : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            transform.parent.GetComponent<RatOnGround>().EnemyDetected(other.transform);
        }
    }
}
