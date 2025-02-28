using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform throwPoint; 
    public float throwForce = 10f;

    private Item currentItem;
    private Item oldItem;

    void Update()
    {
        if (currentItem == null && Input.GetKeyDown(KeyCode.E))
        {
            TryPickUp();
        }
        else if (currentItem != null && Input.GetMouseButtonDown(1)) // Prawy przycisk myszy
        {
            ThrowItem();
        }
    }

    private void TryPickUp()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D col in items)
        {
            Item item = col.GetComponent<Item>();
            if (item != null)
            {
                PickUpItem(item);
                break;
            }
        }
    }

    private void PickUpItem(Item item)
    {
        currentItem = item;
        item.gameObject.SetActive(false);
        
        Debug.Log("Podniesiono: " + item.itemName);
        oldItem = item;
        //Destroy(item);
    }

    private void ThrowItem()
    {
        GameObject thrownItem = Instantiate(currentItem.gameObject, throwPoint.position, Quaternion.identity);
        
        thrownItem.SetActive(true);
        oldItem = currentItem;
        Destroy(oldItem.gameObject);

        Rigidbody2D rb = thrownItem.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 throwDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            rb.linearVelocity = throwDirection * throwForce;
            rb.linearDamping = 1f; 
        }

        currentItem = null; // Gracz nie trzyma już przedmiotu
    }
}

