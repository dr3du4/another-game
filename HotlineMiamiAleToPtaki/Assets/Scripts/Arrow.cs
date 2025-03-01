 using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;  
    public float maxDistance = 10f; 
    private Vector2 startPosition;  
    private Vector2 direction;  

    public void Initialize(Vector2 targetPosition)
    {
        startPosition = transform.position;
        direction = (targetPosition - startPosition).normalized; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); 
    }

    private void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime; 
        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Destroy(gameObject);
    }
}

