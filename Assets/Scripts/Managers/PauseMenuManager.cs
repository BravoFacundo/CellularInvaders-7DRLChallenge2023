using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private PauseMenuUI pauseMenuUI;
    [SerializeField] private RetryMenuUI retryMenuUI;

    [Header("Wwise Events Strings")]
    [SerializeField] private string buttonClickSFX;
    [SerializeField] private string buttonHoverSFX;

    [Header("Slider References")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;

    private void Awake() => Resume();

    private void Update()
    {
        // Slider Float 0-100 | Default Value = 100 () | Whole Numbers = True
        
        AkSoundEngine.SetRTPCValue("RTPC_MasterVolume", sliderMaster.value);
        AkSoundEngine.SetRTPCValue("RTPC_SFXVolume", sliderSFX.value);
        AkSoundEngine.SetRTPCValue("RTPC_MusicVolume", sliderMusic.value);

        HandlePausingGame();
    }

    public void OnMouseClick()
    {
        AkSoundEngine.PostEvent(buttonClickSFX, gameObject);
    }

    public void OnMouseHover()
    {
        AkSoundEngine.PostEvent(buttonHoverSFX, gameObject);
    }

    private void HandlePausingGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    private void Resume()
    {
        pauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ContinueGame()
    {
        if (GameIsPaused) Resume();
    }

    public void LoseGame()
    {
        retryMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
