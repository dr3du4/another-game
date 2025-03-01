using UnityEngine;
using UnityEngine.UI;

public class SkipTutorial : MonoBehaviour
{
    public Slider skipSlider;
    float value = 0f;
    public int skipSpeed = 5;
    public GameObject tutorialCanvas;

    private void Start()
    {
        skipSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if (!skipSlider.isActiveAndEnabled)
                skipSlider.gameObject.SetActive(true);
            value += skipSpeed *  Time.deltaTime;
            skipSlider.value = value;
        }
        else if(Input.GetKeyUp(KeyCode.Escape))
        {
            value = 0;
            skipSlider.value = value;
            skipSlider.gameObject.SetActive(false);
        }

        if (value >= skipSlider.maxValue)
        {
            Time.timeScale = 1;
            tutorialCanvas.SetActive(false);
        }
    }
}
