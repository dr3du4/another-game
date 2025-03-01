using UnityEngine;
using UnityEngine.UI;


public class healthBar : MonoBehaviour
{
    public Slider healthSlider; 
    public PlayerStats playerStats; 

    void Start()
    {
        playerStats=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        
        healthSlider.maxValue = playerStats.maxHealth;
        healthSlider.value = playerStats.health;
    }

    void Update()
    {
        
        healthSlider.value = playerStats.health;
    }   
}
