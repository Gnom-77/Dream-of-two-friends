using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        audioMixer.GetFloat("volume", out float mixerVolume);
        _slider.value = mixerVolume;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
