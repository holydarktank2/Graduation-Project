using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer engineMixer;
    public Slider slider;
    public Slider engineslider;
    public Dropdown checkpoint;
    public Dropdown controltype;
    public Dropdown spamount;
    [HideInInspector]
    //public static bool checkpointenable;
   
    void Awake()
    {
        slider.onValueChanged.AddListener(SetVolume);
        engineslider.onValueChanged.AddListener(SetEngineVolume);
    }
    
    public void CheckpointEnable(int index)
    {
        PlayerPrefs.SetInt("checkpointenable", index);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("mainvolume", slider.value); 
        PlayerPrefs.SetFloat("enginevolume", engineslider.value);
    }

    public void SetVolume (float sliderValue)
    {
        //audioMixer.SetFloat("volume",Mathf.Log10(sliderValue) * 20);
        //engineMixer.SetFloat("enginevolume", Mathf.Log10(sliderValue) * 20);
        //PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        AudioListener.volume = slider.value;
        PlayerPrefs.SetFloat("mainvolume", slider.value);
    }
    public void SetEngineVolume(float sliderValue)
    {
        //engineslider.maxValue = slider.value;
        //engineMixer.SetFloat("enginevolume", Mathf.Log10(sliderValue) * 20); 
        PlayerPrefs.SetFloat("enginevolume", engineslider.value);
    }
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("mainvolume", slider.value);
        engineslider.value = PlayerPrefs.GetFloat("enginevolume", engineslider.value);
        checkpoint.value = PlayerPrefs.GetInt("checkpointenable");
        controltype.value = PlayerPrefs.GetInt("controlType");
        spamount.value = PlayerPrefs.GetInt("spawnamount");
    }

    public void ChangeControl(int index)
    {
        PlayerPrefs.SetInt("controlType", index);
    }

    public void SpawnAmount(int index)
    {
        PlayerPrefs.SetInt("spawnamount", index);
    }

}
