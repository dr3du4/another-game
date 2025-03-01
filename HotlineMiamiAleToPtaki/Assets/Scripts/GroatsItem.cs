using UnityEngine;

public class GroatsItem : Item
{
    [SerializeField]
    GameObject groundGroutsPrefab;
    [SerializeField]
    Sprite flyingGroatsSprite;

    protected override void OnItemThrown()
    {
        base.OnItemThrown();
        GetComponentInChildren<SpriteRenderer>().sprite = flyingGroatsSprite;
    }

    protected override void OnItemFinishedFlying()
    {
        base.OnItemFinishedFlying();
        Instantiate(groundGroutsPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
