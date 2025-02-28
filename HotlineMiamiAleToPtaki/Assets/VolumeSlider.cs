using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Wczytujemy zapisany poziom głośności (domyślnie 100%)
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume", 1f);
        AudioListener.volume = volumeSlider.value;

        // Dodajemy event na zmianę wartości suwaka
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume);
        PlayerPrefs.Save();
    }
}