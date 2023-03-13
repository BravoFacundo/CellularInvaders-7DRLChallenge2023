using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    private void Start()
    {
        AkSoundEngine.PostEvent("Play_Music", gameObject);
        AkSoundEngine.PostEvent("Play_ENV_AmbienceBG", gameObject);
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
        SceneManager.LoadScene("InGame");
    }

    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
