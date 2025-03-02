using UnityEngine;

public class AttackRadiusDetection : MonoBehaviour
{
    Enemy enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        return;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
