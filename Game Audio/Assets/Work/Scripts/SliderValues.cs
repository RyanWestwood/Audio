using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValues : MonoBehaviour
{
    public string keyname;
    public TextMeshProUGUI title;
    public Slider slider;

    void Start()
    {
        title.text = keyname;
        slider.value = PlayerPrefs.GetFloat(keyname, 1);
    }

    public void SaveSliderValue(System.Single value)
    {
        PlayerPrefs.SetFloat(keyname, value);
    }
}