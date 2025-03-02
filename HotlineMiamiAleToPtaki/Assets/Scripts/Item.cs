using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    protected DamageArea damageArea;

   
    public string itemName;
    public bool isHeld = false;
    public bool isThrowed = false;
    public int durability = 5;
    protected virtual void Awake()
    {
        gameObject.SetActive(true);
        damageArea = GetComponentInChildren<DamageArea>();
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

    public bool canPickUp()
    {
        return !isHeld && !isThrowed;
    }

    public void UseItem()
    {
        itemUse();
        Debug.Log("durability: " + durability);
        durability--;
        if (durability <= 0)
        {
            DestroyItem();
        }
    }

    protected virtual void itemUse()
    {
        Debug.Log("item use");
    }

    protected virtual bool CanThrow(){
        return isHeld && !isThrowed;
    }

    public void tryThrowItem(Vector2 throwDirection, float force, float zRotation){
        if (!CanThrow()) return;
        isHeld = false;
        isThrowed = true;
        transform.SetParent(null);
        
        transform.Rotate(0, 0, zRotation);

        ThrowItem(throwDirection, force);
    }

    protected virtual void ThrowItem(Vector2 throwDirection, float force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        OnItemThrown();
        damageArea.isDealingDamage = true;
        if (rb != null)
        {
            Debug.Log("durability: " + durability);
            durability--;
            if (durability <= 0)
            {
                DestroyItem();
            }
            rb.linearVelocity = throwDirection * force;
            StartCoroutine(ReduceVelocity(rb));
        }
    }

    private IEnumerator ReduceVelocity(Rigidbody2D rb){
        while(rb.linearVelocity.magnitude > 2f){
            rb.linearVelocity *= 0.9f;
            yield return new WaitForSeconds(0.1f);
        }
        rb.linearVelocity = Vector2.zero;

        isThrowed = false;
        damageArea.isDealingDamage = false;

        OnItemFinishedFlying();
    }

    protected virtual void OnItemThrown(){
        Debug.Log("Item throwed");
    }

    protected virtual void OnItemFinishedFlying(){
        Debug.Log("Item finished flying");
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }
}