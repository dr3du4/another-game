using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public bool isThrowed = false;
    private void Awake()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity *= 5f;
            rb.linearDamping = 5f;
        }
    }

    public void itemUse()
    {
        Debug.Log("item use");
    }
}