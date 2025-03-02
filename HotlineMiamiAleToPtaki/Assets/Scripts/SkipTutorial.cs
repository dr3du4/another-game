using UnityEngine;
using UnityEngine.UI;

public class SkipTutorial : MonoBehaviour
{
    public Slider skipSlider;
    private float value = 0f;
    public int skipSpeed = 5;
    public GameObject tutorialCanvas;

    private void Start()
    {
        skipSlider.gameObject.SetActive(false);
        if (tutorialCanvas.activeSelf)
        {
            PauseGame(); // Zatrzymuje grę na początku, jeśli tutorial jest aktywny
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!skipSlider.isActiveAndEnabled)
                skipSlider.gameObject.SetActive(true);

            value += skipSpeed * Time.unscaledDeltaTime; // UnscaledDeltaTime ignoruje Time.timeScale
            skipSlider.value = value;
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            ResetSkip();
        }

        if (value >= skipSlider.maxValue)
        {
            ResumeGame(); // Wyłączamy tutorial i wznawiamy grę
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        tutorialCanvas.SetActive(false);
        skipSlider.gameObject.SetActive(false);
        value = 0;
    }

    private void ResetSkip()
    {
        value = 0;
        skipSlider.value = value;
        skipSlider.gameObject.SetActive(false);
    }
}