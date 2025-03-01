using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    [SerializeField]
    float useCooldown = 2f;

    bool canUse = true;
    public string itemName;
    public bool isHeld = false;
    public bool isThrowed = false;
    protected virtual void Awake()
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

    public bool canPickUp()
    {
        return !isHeld && !isThrowed;
    }

    public void tryUseItem()
    {
        if(canUse){
            itemUse();
            canUse = false;
            Invoke("resetUse", useCooldown);
        }
    }

    private void resetUse(){
        canUse = true;
    }

    protected virtual void itemUse(){
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
        OnItemThrowed();

        transform.Rotate(0, 0, zRotation);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
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

        OnItemFinishedFlying();
    }

    protected void OnItemThrowed(){
        Debug.Log("Item throwed");
    }

    protected void OnItemFinishedFlying(){
        Debug.Log("Item finished flying");
    }
}