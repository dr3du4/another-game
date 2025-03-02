using UnityEngine;

public class PlayerDamagingArea : MonoBehaviour
{
    public bool isDealingDamage = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && isDealingDamage)
        {
            other.GetComponent<playerMovement>().TakeDamage();
        }
    }
}
