using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle soundToggle;
    public Slider volumeSlider;

    private const string SOUND_KEY = "SoundEnabled";
    private const string VOLUME_KEY = "Volume";

    void Start()
    {
        bool soundEnabled = PlayerPrefs.GetInt(SOUND_KEY, 1) == 1;
        float volume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);

        soundToggle.isOn = soundEnabled;
        volumeSlider.value = volume;

        ApplySettings();

        soundToggle.onValueChanged.AddListener(OnToggleChanged);
        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnToggleChanged(bool value)
    {
        PlayerPrefs.SetInt(SOUND_KEY, value ? 1 : 0);
        ApplySettings();
    }

    void OnSliderChanged(float value)
    {
        PlayerPrefs.SetFloat(VOLUME_KEY, value);
        ApplySettings();
    }

    void ApplySettings()
    {
        bool soundOn = PlayerPrefs.GetInt(SOUND_KEY, 1) == 1;
        float vol = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
        AudioListener.volume = soundOn ? vol : 0f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}