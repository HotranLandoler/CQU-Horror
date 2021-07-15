using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private readonly string volumeKey = "Volume";

    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey(volumeKey))
        {
            volumeSlider.value = PlayerPrefs.GetFloat(volumeKey);
            AudioListener.volume = volumeSlider.value;
        }
        else
        {
            volumeSlider.value = AudioListener.volume;
        }
        volumeSlider.onValueChanged.AddListener(SaveValue);
    }

    private void SaveValue(float val)
    {
        AudioListener.volume = val;
        PlayerPrefs.SetFloat(volumeKey, val);
    }

    private void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveListener(SaveValue);
    }
}
