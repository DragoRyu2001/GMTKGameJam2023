using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle volumeToggle;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle sfxToggle;
    [SerializeField] TMPro.TextMeshProUGUI muteIndicator;
    [SerializeField] TMPro.TextMeshProUGUI muteIndicator2;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener((v)=>OnSliderValueChanged(v));
        volumeToggle.onValueChanged.AddListener((v) => OnToggle(v));
        sfxSlider.onValueChanged.AddListener(v => OnSliderValueChanged2(v));
        sfxToggle.onValueChanged.AddListener((v) => OnToggle2(v));
    }

    public void OnSliderValueChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value); 
    }

    public void OnToggle(bool isOn)
    {
        AudioManager.Instance.MuteUnmuteMusic(!isOn);
        muteIndicator.text = isOn ? "ON" : "OFF";
    }
    public void OnSliderValueChanged2(float value)
    {
        AudioManager.Instance.SetSFXVolume(value); 
    }

    public void OnToggle2(bool isOn)
    {
        AudioManager.Instance.MuteUnmuteSFX(!isOn);
        muteIndicator.text = isOn ? "ON" : "OFF";
    }
}
