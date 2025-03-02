using System;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Item
{
    public int amunitionAmount;
    public List<GameObject> amunitions;
    public Transform shootPoint;  
    private void Start()
    {
        amunitions = new List<GameObject>(amunitionAmount);
    }

    protected override void itemUse()
    {
        if (amunitions.Count > 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject arrow = Instantiate(amunitions[0], shootPoint.position, shootPoint.rotation);
            arrow.GetComponent<Arrow>().Initialize(mousePosition);
            amunitions.RemoveAt(0); 
        }
    }
}
