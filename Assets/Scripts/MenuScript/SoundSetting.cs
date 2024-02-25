using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 50));
    }

    // Update is called once per frame
    public void SetVolume(float _value) 
    {
        if(_value < 1)
        {
            _value = .001f;
        }
        RefrestSlider(_value);
        PlayerPrefs.SetFloat("SaveMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value/100)*20f);
    }
    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
    }
    public void RefrestSlider(float _value)
    {
        soundSlider.value = _value;
    }
}
