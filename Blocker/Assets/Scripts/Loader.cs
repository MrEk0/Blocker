using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject audioManager;
    public GameObject sceneManager;

    private void Awake()
    {
        if (AudioManager.instance == null)
            Instantiate(audioManager);

        if (SceneLoader.instance == null)
            Instantiate(sceneManager);
    }
}
