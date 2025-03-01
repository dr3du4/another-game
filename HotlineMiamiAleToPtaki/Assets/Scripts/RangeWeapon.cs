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
        // Sprawdzamy, czy mamy amunicję (czy jest strzała do wystrzelenia)
                if (amunitions.Count > 0)
                {
                    GameObject arrow = Instantiate(amunitions[0], shootPoint.position, shootPoint.rotation);
                    
                    amunitions.RemoveAt(0); 
                }
    }
}
