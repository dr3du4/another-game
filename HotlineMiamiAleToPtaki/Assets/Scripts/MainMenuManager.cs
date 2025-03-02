using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject optionsPanel; // Panel opcji

    public void StartGame()
    {
        SceneManager.LoadScene("Cut_scene 1"); // Zamień na nazwę swojej sceny gry
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Gra zamknięta"); // Działa tylko po zbudowaniu gry
    }
}