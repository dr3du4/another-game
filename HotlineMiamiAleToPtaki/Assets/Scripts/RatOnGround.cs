using UnityEngine;

public class RatOnGround : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    Transform target;
    public void EnemyDetected(Transform enemyTransform)
    {
        target = enemyTransform;
    }

    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().RegisterHit();
            Destroy(gameObject);
        }
    }
}
