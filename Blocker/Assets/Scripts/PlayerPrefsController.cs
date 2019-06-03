using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string VOLUME_KEY = "volume";
    const string GRAPHICS_KEY = "graphics";
    const string MUSIC_KEY = "music";
    const string BESTSCORE_KEY = "best score";

    public static void SetVolume(float volume)
    {
        //if(volume<= && volume>= )
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public static float GetVolume()
    {
        return PlayerPrefs.GetFloat(VOLUME_KEY);
    }

    public static void SetBestScore(int score)
    {
        PlayerPrefs.SetInt(BESTSCORE_KEY, score);
        PlayerPrefs.Save();
    }

    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt(BESTSCORE_KEY, 0);
    }
}
