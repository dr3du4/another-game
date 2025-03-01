using UnityEngine;

public class DamageArea : MonoBehaviour
{
    Collider2D hitArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitArea = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().RegisterHit();
        }
    }
}
