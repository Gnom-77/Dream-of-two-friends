using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _audioCount;

    private void OnEnable()
    {
        audioMixer.GetFloat("volume", out float mixerVolume);
        _slider.value = mixerVolume;
        SetAudioCount(mixerVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        SetAudioCount(volume);
    }

    private void SetAudioCount(float mixerVolume)
    {
        _audioCount.text = $"{mixerVolume + 80:F0}";
    }
}
