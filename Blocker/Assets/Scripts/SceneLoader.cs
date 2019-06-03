using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance = null;
    // Start is called before the first frame update
    void Start()
    {
        //if (instance != null && instance != this)
        //{
        //    //...destroy this and exit. There can be only one UIManager
        //    Destroy(gameObject);
        //    return;
        //}

        //instance = this;
        //DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);//this
    }

    public void LoadMainMenuScene()
    {
        AudioManager.PlayMouseSound();
        SceneManager.LoadScene(0);
    }

    public void LoadFirstLevel()
    {
        AudioManager.PlayMouseSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartScene()
    {
        AudioManager.PlayMouseSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        AudioManager.PlayMouseSound();
        Application.Quit();
    }

}
