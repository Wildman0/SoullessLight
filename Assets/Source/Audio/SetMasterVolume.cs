using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMasterVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider slider;
    public Button buttonSelect;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("masterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
            buttonSelect.Select();

    }
}