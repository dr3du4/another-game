using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform throwPoint; 
    public float throwForce = 10f;

    private Item currentItem;
    private Item oldItem;
    [SerializeField]
    GameObject itemHolder;


    void Update()
    {
        if (currentItem == null && Input.GetKeyDown(KeyCode.Space))
        {
            TryPickUp();
            Debug.Log("trying to pick up item");
        }
        else if (currentItem != null && Input.GetMouseButtonDown(1)) // Prawy przycisk myszy
        {
            ThrowItem();
        }
        else if (currentItem != null && Input.GetMouseButtonDown(0))
        {
            currentItem.tryUseItem();
        }
    }

    private void TryPickUp()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 1f);

        Debug.Log("Items: " + items.Length);
        foreach (Collider2D col in items)
        {
            Item item = col.GetComponent<Item>();
            Debug.Log("Item: " + col.gameObject.name);
            if (item != null && item.canPickUp())
            {
                PickUpItem(item);
                break;
            }
        }
    }

    private void PickUpItem(Item item)
    {
        currentItem = item;
        item.transform.SetParent(itemHolder.transform);
        item.isThrowed = false;
        item.isHeld = true;
        Debug.Log("Podniesiono: " + item.itemName);
        oldItem = item;
        //Destroy(item);
    }

    private void ThrowItem()
    {
        //GameObject thrownItem = Instantiate(currentItem.gameObject, throwPoint.position, Quaternion.identity);
        currentItem.transform.SetParent(null);
        currentItem.isThrowed = true;
        currentItem.isHeld = false;
        //thrownItem.SetActive(true);
        //thrownItem.GameObject().GetComponent<Item>().isThrowed = true;
        //oldItem = currentItem;
        //Destroy(oldItem.gameObject);

        Rigidbody2D rb = currentItem.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 throwDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            rb.linearVelocity = throwDirection * throwForce;
            rb.linearDamping = 1f; 
        }

        currentItem = null; // Gracz nie trzyma już przedmiotu
    }
}

