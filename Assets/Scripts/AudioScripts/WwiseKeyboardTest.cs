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
    
    void Start ()
    {
        AkSoundEngine.SetState("GameStatus", "InMenu");
        AkSoundEngine.SetRTPCValue("RTPC_PlayerLife", 3f);
        Debug.Log("Wwise State 'GameStatus' set to InMenu from " + gameObject);
        Debug.Log("Wwise RTPC 'PlayerLife' set to 3 from " + gameObject);
    }
    void Update()
    {
        //Plays the 1kHz beep test when Q is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AkSoundEngine.PostEvent(testBeepHigh, gameObject);
            Debug.Log("Wwise Event " + testBeepHigh + " has been played in " + gameObject + " thus pressing Q");
        }
        //Plays the 500Hz beep test when W is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            AkSoundEngine.PostEvent(testBeepLow, gameObject);
            Debug.Log("Wwise Event " + testBeepLow + " has been played in " + gameObject + " thus pressing W");
        }
        //Wwise State GameStatus set to InGame when G is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            AkSoundEngine.SetState("GameStatus", "InGame");
            Debug.Log("Wwise State 'GameStatus' set to 'InGame' from " + gameObject + " thus pressing G");
        }
        //Wwise State GameStatus set to InMenu when M is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            AkSoundEngine.SetState("GameStatus", "InMenu");
            Debug.Log("Wwise State 'GameStatus' set to 'InMenu' from " + gameObject + " thus pressing M");
        }
        //Sets Wwise RTPC PlayerLife to 0 when Aplhanumeric 3 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            AkSoundEngine.SetRTPCValue("RTPC_PlayerLife", 0f);
            Debug.Log("Wwise RTPC 'PlayerLife' set to 0 from " + gameObject + "thus pressing 0");
        }
        //Sets Wwise RTPC PlayerLife to 1 when Aplhanumeric 1 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AkSoundEngine.SetRTPCValue("RTPC_PlayerLife", 1f);
            Debug.Log("Wwise RTPC 'PlayerLife' set to 1 from " + gameObject + " thus pressing 1");
        }
        //Sets Wwise RTPC PlayerLife to 2 when Aplhanumeric 2 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AkSoundEngine.SetRTPCValue("RTPC_PlayerLife", 2f);
            Debug.Log("Wwise RTPC 'PlayerLife' set to 2 from " + gameObject + " thus pressing 2");
        }
        //Sets Wwise RTPC PlayerLife to 3 when Aplhanumeric 2 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AkSoundEngine.SetRTPCValue("RTPC_PlayerLife", 3f);
            Debug.Log("Wwise RTPC 'PlayerLife' set to 3 from " + gameObject  + " thus pressing 3");
        }
        //Plays and stops the drumkit loop test when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (musicIsPlaying == false)
            {
                AkSoundEngine.SetState("GameStatus", "InMenu");
                AkSoundEngine.PostEvent(playTestMusic, gameObject);
                musicIsPlaying = !musicIsPlaying;
                Debug.Log("Wwise Event " + playTestMusic + " has been played in " + gameObject  + " thus pressing space bar");
                Debug.Log("Wwise State 'GameStatus' set to 'InMenu' from " + gameObject  + " thus pressing space bar");
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
