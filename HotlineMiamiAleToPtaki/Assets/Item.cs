using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    float useCooldown = 2f;

    bool canUse = true;
    public string itemName;
    public bool isHeld = false;
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
}