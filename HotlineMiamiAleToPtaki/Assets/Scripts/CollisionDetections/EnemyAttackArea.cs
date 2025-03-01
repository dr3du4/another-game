using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<playerMovement>().TakeDamage();
        }
    }
}
