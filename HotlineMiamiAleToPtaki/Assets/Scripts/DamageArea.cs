using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public bool isDealingDamage = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") && isDealingDamage)
        {
            other.GetComponent<Enemy>().RegisterHit();
        }
    }
}
