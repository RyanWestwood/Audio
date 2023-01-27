using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValues : MonoBehaviour
{
    [Header("Properties")]
    public TextMeshProUGUI title;

    [Header("FMOD Properties")]
    public string audio_bus_id;
    private FMOD.Studio.Bus audio_bus;

    [Header("Slider Properties")]
    public Slider slider;
    public string keyname;
    private float volume;

    void Start()
    {
        title.text = keyname;
        volume = PlayerPrefs.GetFloat(keyname, 1);
        slider.value = volume;
        audio_bus = FMODUnity.RuntimeManager.GetBus("bus:/" + audio_bus_id);
    }

    public void SaveSliderValue(System.Single value)
    {
        PlayerPrefs.SetFloat(keyname, value);
        volume = value;
        UpdateSoundVolume();
    }

    private void UpdateSoundVolume()
    {
        audio_bus.setVolume(volume); 
    }
}