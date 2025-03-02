using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class doorScene : MonoBehaviour
{
    public string nextScene; // Nazwa sceny, do której przechodzimy

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jeśli gracz wejdzie w obszar drzwi
        if (other.CompareTag("Player"))
        {
            
            // Sprawdzamy, czy nie ma już przeciwników
            
            if (NoEnemiesLeft())
            { 
                SceneManager.LoadScene(nextScene); // Przechodzimy do kolejnej sceny
            }
        }
    }

    // Sprawdza, czy nie ma już wrogów na scenie
    private bool NoEnemiesLeft()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); 
        return enemies.All(enemy => enemy.isDead);
    }
}