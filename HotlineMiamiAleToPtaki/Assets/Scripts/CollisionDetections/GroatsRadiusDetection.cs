using UnityEngine;

public class GroatsRadiusDetection : MonoBehaviour
{
    Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Groats"))
        {
            enemy.OnEnteredGroatsDetectionRadius(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Groats"))
        {
            enemy.OnExitedGroatsDetectionRadius();
        }
    }
}
