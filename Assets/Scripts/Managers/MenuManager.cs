using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Wwise Events Strings")]
    [SerializeField] private string buttonClickSFX;
    [SerializeField] private string buttonHoverSFX;
    [SerializeField] private string startSFX;

    private void Awake()
    {
        AkSoundEngine.SetState("GameStatus", "InMenu");
    }

    public void OnMouseClick()
    {            
        AkSoundEngine.PostEvent(buttonClickSFX, gameObject);
    }

    public void OnMouseHover()
    {
        AkSoundEngine.PostEvent(buttonHoverSFX, gameObject);
    }

    public void OnGameStart()
    {
        AkSoundEngine.PostEvent(startSFX, gameObject);
        AkSoundEngine.SetState("GameStatus", "InGame");
    }

}
