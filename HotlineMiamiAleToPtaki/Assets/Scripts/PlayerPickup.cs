using System;
using Unity.Mathematics;
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
        }
        else if (currentItem != null && Input.GetMouseButtonDown(1)) // Prawy przycisk myszy
        {
            Vector2 throwDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            currentItem.tryThrowItem(throwDirection, throwForce, transform.rotation.z);
            currentItem = null;
        }
        else if (currentItem != null && Input.GetMouseButtonDown(0))
        {
            currentItem.tryUseItem();
        }
    }

    private void TryPickUp()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D col in items)
        {
            Item item = col.GetComponent<Item>();
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
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        Debug.Log("Podniesiono: " + item.itemName);
    }
}

