using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolum : MonoBehaviour
{
    public AudioMixer masterMixer;
    public AudioMixer effectMixer;

    public Slider musicSlider;
    public Slider effectSlider;

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMasterVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
    }

    public void SetMasterVolume(float volume)
    {
        if (volume <= 0)
            masterMixer.SetFloat("musicMaster", -80);
        else
            masterMixer.SetFloat("musicMaster", Mathf.Log10(volume) * 20);
    }

    public void SetEffectVolume(float volume)
    {
        if (volume <= 0)
            effectMixer.SetFloat("effectMaster", -80);
        else
            effectMixer.SetFloat("effectMaster", Mathf.Log10(volume) * 20);
    }
}