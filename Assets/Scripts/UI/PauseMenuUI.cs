using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    private void OnEnable()
    {
        AkSoundEngine.SetState("GameStatus", "InMenu");
    }

    private void OnDisable()
    {
        AkSoundEngine.SetState("GameStatus", "InGame");
    }

    public void OnContinue() => FindObjectOfType<PauseMenuManager>().ContinueGame();
    public void OnRetry() => SceneManager.LoadScene("InGame");
    public void OnExitMenu() => SceneManager.LoadScene("Menu");
}
