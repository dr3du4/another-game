using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<playerMovement>().TakeDamage();
        }
    }
}
