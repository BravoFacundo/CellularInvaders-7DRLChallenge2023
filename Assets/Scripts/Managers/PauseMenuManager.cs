using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Wwise Events Strings")]
    [SerializeField] private string buttonClickSFX;
    [SerializeField] private string buttonHoverSFX;

    [Header("Slider References")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;

    private void Update()
    {
        // Slider Float 0-100 | Default Value = 100 () | Whole Numbers = True
        AkSoundEngine.SetRTPCValue("RTPC_MasterVolume", sliderMaster.value);
        AkSoundEngine.SetRTPCValue("RTPC_SFXVolume", sliderSFX.value);
        AkSoundEngine.SetRTPCValue("RTPC_MusicVolume", sliderMusic.value);
    }

    public void OnMouseClick()
    {
        AkSoundEngine.PostEvent(buttonClickSFX, gameObject);
    }

    public void OnMouseHover()
    {
        AkSoundEngine.PostEvent(buttonHoverSFX, gameObject);
    }
}
