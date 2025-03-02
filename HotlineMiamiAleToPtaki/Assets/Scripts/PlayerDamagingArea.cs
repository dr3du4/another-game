using UnityEngine;

public class PlayerDamagingArea : MonoBehaviour
{
    public bool isDealingDamage = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && isDealingDamage)
        {
            other.GetComponent<playerMovement>().TakeDamage();
        }
    }
}
