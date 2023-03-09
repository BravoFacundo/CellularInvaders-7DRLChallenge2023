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

    public void OnMouseHover(GameObject textObj)
    {
        AkSoundEngine.PostEvent(buttonHoverSFX, gameObject);
        textObj.GetComponent<TMP_Text>().outlineWidth = 0.3f;
    }
    public void OnMouseHoverExit(GameObject textObj)
    {
        textObj.GetComponent<TMP_Text>().outlineWidth = 0.2f;
    }

    public void OnGameStart()
    {
        AkSoundEngine.PostEvent(startSFX, gameObject);
        AkSoundEngine.SetState("GameStatus", "InGame");
    }

}
