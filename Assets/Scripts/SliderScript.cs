using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private AudioMixer _mixer; // assign this in inspector
    [SerializeField] private string exposedParam = "MasterVolume"; // or MusicVolume, etc.

    // void Start()
    // {

    //     // Set default value to 100
    // _slider.value = 100;

    // // Update text immediately
    // _sliderText.text = _slider.value.ToString("0");
    //     _slider.onValueChanged.AddListener((v) => {
    //         _sliderText.text = v.ToString("0");
    //     });
    // }
    void Start()
    {
        // Load previously saved volume
        float savedVolume = PlayerPrefs.GetFloat(exposedParam, 1.0f);
        _slider.value = savedVolume * 100f; 
        _sliderText.text = _slider.value.ToString("0");
        SetVolume(_slider.value);

        _slider.onValueChanged.AddListener((v) => {
            _sliderText.text = v.ToString("0");
            SetVolume(v);
        });
    }

    void SetVolume(float sliderValue)
{
    // Clamp to a minimum safe value (0.0001 = -80dB)
    float clampedValue = Mathf.Clamp(sliderValue / 100f, 0.0001f, 1f);
    float volume = Mathf.Log10(clampedValue) * 20;

    _mixer.SetFloat(exposedParam, volume);
    PlayerPrefs.SetFloat(exposedParam, clampedValue);
}
}
