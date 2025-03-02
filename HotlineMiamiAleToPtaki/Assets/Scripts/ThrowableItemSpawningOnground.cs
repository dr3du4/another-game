using UnityEngine;

public class ThrowableItemSpawningOnground : Item
{
    [SerializeField]
    GameObject groundObjectPrefab;
    [SerializeField]
    Sprite flyingItemSprite;

    protected override void OnItemThrown()
    {
        base.OnItemThrown();
        GetComponentInChildren<SpriteRenderer>().sprite = flyingItemSprite;
    }

    protected override void OnItemFinishedFlying()
    {
        base.OnItemFinishedFlying();
        Instantiate(groundObjectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
