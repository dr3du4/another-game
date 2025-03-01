using UnityEngine;

public class Bow : RangeWeapon
{
    GameObject arrowPrefab;
    void Start()
    {
        for (int i = 0; i < amunitionAmount; i++)
        {
            amunitions.Add(arrowPrefab); // Dodajemy "null" jako placeholder dla amunicji
        }
    }
}
