using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseKeyboardTest : MonoBehaviour
{
    //String with the name of the 1kHz beep test Wwise Event
    [SerializeField] private string testBeepHigh;
    //String with the name of the 500Hz beep test Wwise Event
    [SerializeField] private string testBeepLow;
    //String with name of the play Drumkit loop test Wwise Event
    [SerializeField] private string playTestMusic;
    //String with name of the stop Drumkit loop test Wwise Event
    [SerializeField] private string stopTestMusic;

    private bool musicIsPlaying = false;
    
    void Update()
    {
        //Plays the 1kHz beep test when Alphanumeric 1 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AkSoundEngine.PostEvent(testBeepHigh, gameObject);
            Debug.Log("Wwise Event " + testBeepHigh + " has been played in " + gameObject);
        }
        //Plays the 500Hz beep test when Alphanumeric 2 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AkSoundEngine.PostEvent(testBeepLow, gameObject);
            Debug.Log("Wwise Event " + testBeepLow + " has been played in " + gameObject);
        }
        //Plays and stops the drumkit loop test when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (musicIsPlaying == false)
            {
                AkSoundEngine.PostEvent(playTestMusic, gameObject);
                musicIsPlaying = !musicIsPlaying;
                Debug.Log("Wwise Event " + playTestMusic + " has been played in " + gameObject);
            }
            else
            {
                AkSoundEngine.PostEvent(stopTestMusic, gameObject);
                musicIsPlaying = !musicIsPlaying;
                Debug.Log("Wwise Event " + stopTestMusic + " has been played in " + gameObject);
            }
        }

    }
}
