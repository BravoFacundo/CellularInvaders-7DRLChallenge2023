using UnityEngine;

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
}
